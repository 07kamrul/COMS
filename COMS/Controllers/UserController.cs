﻿using AutoMapper;
using COMS.Security;
using Core.Common;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Serilog;
using System;
using System.Collections.Generic;
using static Core.Common.Enums;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly Config _config;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserController(Config config, IUserService userService, ILogger logger, IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        private int GenerateInvitationCode()
        {
            return (12 % DateTime.Now.Month) + DateTime.Now.Year;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        [HttpPost("signup")]
        public dynamic SignUp([FromBody] UserRequestModel userRequestModel)
        {
            return SignUp(userRequestModel, GenerateInvitationCode());
        }

        [AllowAnonymous]
        [HttpPost("InvitationSignUp")]
        private dynamic SignUp(UserRequestModel userRequestModel, int invitationCode)
        {
            _logger.Information("User signup started");
            if(GenerateInvitationCode() != invitationCode)
            {
                throw new BadHttpRequestException("Invitation code is incorrect");
            }

            try
            {
                int tokenLifeSpanInMinutes = int.Parse(_config.Jwt.TokenLifespan);

                if(userRequestModel.Email == null || userRequestModel.Password == null)
                {
                    throw new BadHttpRequestException("This email or password is incorrect");
                }

                if (_userService.IsExistingUser(userRequestModel.Email))
                {
                    throw new BadHttpRequestException("This email is already in use.");
                }

                userRequestModel.Password = CryptoService.EncryptText(userRequestModel.Password);
                var USER = _userService.SaveUser(userRequestModel);
                
                return User;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        [ClaimRequirement(PermissionType.Maker, PermissionType.Admin)]
        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserRequestModel userRequestModel)
        {
            _logger.Information($"Updating User: {userRequestModel.Email}");
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new BadHttpRequestException("invalid request");
                }

                userRequestModel.Password = string.IsNullOrEmpty(userRequestModel.Password) ? null : CryptoService.EncryptText(userRequestModel.Password);
                _userService.UpdateUser(userRequestModel);
                
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
            finally
            {
                _logger.Information($"Successfully updated user: {userRequestModel.Email}");
            }
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("Roles")]
        public List<RoleResponse> GetRoles()
        {
            return _userService.GetRoles();
        }

        [ClaimRequirement(PermissionType.Admin)]
        [HttpDelete]
        public void DeleteUser(int id)
        {
            _userService.DeleteUser(id);
        }

        [ClaimRequirement(PermissionType.Admin, PermissionType.Checker, PermissionType.Maker, PermissionType.Viewer)]
        [HttpGet("User")]
        public UserResponse GetLoggedInUser()
        {
            try
            {
                UserResponse user = _mapper.Map<UserResponse>(_userService.GetbyEmail(GetLoggedInUserEmail()));
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return null;
            }
        }

        [ClaimRequirement(PermissionType.Admin)]
        [HttpGet("Users")]
        public Page<UserResponse> GetUserList(string userSearchText, int skip, int take)
        {
            _logger.Information("Get user list started.");
            try
            {
                return _userService.GetUserList(userSearchText, skip, take);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                throw;
            }

        }
    }
}