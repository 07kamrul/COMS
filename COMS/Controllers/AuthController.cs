using AutoMapper;
using Core.Common;
using Core.RequestModels;
using Core.Service;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using static Core.ViewModels.Token;

namespace COMS.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly Config _configuration;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthController(Config configuration, ITokenService tokenService, IUserService userService, ILogger logger, IMapper mapper)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public LoginResult Login([FromBody] TokenRequest tokenRequest)
        {
            _logger.Information("logger started");

            try
            {
                int tokenLifeSpanInMinutes = int.Parse(_configuration.Jwt.TokenLifespan);
                if(tokenRequest.Username == null || tokenRequest.Password == null)
                {
                    return new LoginResult()
                    {
                        IsSuccess = false,
                        Message = "This email or password is incorrect."
                    };
                }

                var user = _userService.GetbyEmail(tokenRequest.Username);
                if(user == null || string.IsNullOrEmpty(user.Email))
                {
                    return new LoginResult() 
                    {
                        IsSuccess = false,
                        Message = "This email is incorrect."
                    };
                }

                if(user.Roles.Count == 0)
                {
                    return new LoginResult()
                    {
                        IsSuccess = false,
                        Message = "You don't have any role assigned. Please contract support."
                    };
                }

                if(string.Compare(CryptoService.DecryptText(user.Password), tokenRequest.Password, false) == 0)
                {
                    if(user.IsActive == false)
                    {
                        return new LoginResult()
                        {
                            IsSuccess = false,
                            Message = "User is inactive in the system."
                        };
                    }

                    var expirationTime = tokenRequest.Remember ? DateTime.UtcNow.AddMinutes(int.Parse(_configuration.Jwt.RememberMeTokenLifespan))
                        : DateTime.UtcNow.AddMinutes(tokenLifeSpanInMinutes);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.FirstName + ' ' +user.LastName),
                        new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(_mapper.Map<List<RoleResponse>>(user.Roles))),
                        new Claim(ClaimTypes.GroupSid, user.GroupId.ToString())
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = _tokenService.GenerateAccessToken(claims, expirationTime);

                    var refreshToken = _tokenService.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    _userService.UpdateRefreshToken(user.Id, refreshToken);

                    return new LoginResult()
                    {
                        IsSuccess = true,
                        Message = "Logged in successfully",
                        Token = token,
                        Roles = _mapper.Map<List<RoleResponse>>(user.Roles.ToList()),
                        TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds()
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new LoginResult()
                {
                    IsSuccess = false,
                    Message = "Something wrong, please contract support."
                };
            }

            return new LoginResult()
            {
                IsSuccess = false,
                Message = "Counld not verify email and password."
            };
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult RequestToken([FromBody] TokenRequest tokenRequest)
        {
            return GetAuthToken(tokenRequest, int.Parse(_configuration.Jwt.TokenLifespan));
        }

        private IActionResult GetAuthToken([FromBody] TokenRequest tokenRequest, int tokenLifeSpanInMinutes)
        {
            _logger.Information("Logger started");

            try
            {
                if(tokenRequest.Username == null || tokenRequest.Password == null)
                {
                    return BadRequest("This email or password is incorrect.");
                }

                var user = _userService.GetbyEmail(tokenRequest.Username);
                if(user == null || string.IsNullOrEmpty(user.Email))
                {
                    return BadRequest("This email or password is incorrect.");
                }

                if(string.Compare(CryptoService.DecryptText(user.Password), tokenRequest.Password, false) == 0)
                {
                    var expirationTime = tokenRequest.Remember ? DateTime.UtcNow.AddDays(int.Parse(_configuration.Jwt.RememberMeTokenLifespan))
                        : DateTime.UtcNow.AddMinutes(tokenLifeSpanInMinutes);

                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.FirstName + ' ' +user.LastName),
                        new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(_mapper.Map<UserResponse>(user.Roles)))
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = _tokenService.GenerateAccessToken(claims, expirationTime);

                    var refreshToken = _tokenService.GenerateRefreshToken();
                    user.RefreshToken = refreshToken;
                    _userService.UpdateUser(_mapper.Map<UserRequest>(user));

                    return Ok(new
                    {
                        Token = token,
                        TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds()
                    });
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return BadRequest("Could not verify email or password.");
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshAccessToken(RefreshTokenRequest refreshTokenRequest)
        {
            _logger.Information("Refresh token started.");

            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);
                var username = principal.Claims.Where(x => x.Type.ToLower() == "email").FirstOrDefault().Value;

                var user = _userService.GetbyEmail(username);
                if(user == null || user.RefreshToken != refreshTokenRequest.RefreshToken)
                {
                    return BadRequest();
                }

                var expirationTime = DateTime.UtcNow.AddMinutes(int.Parse(_configuration.Jwt.TokenLifespan));
                var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims, expirationTime);

                return new ObjectResult(new
                {
                    Token = newJwtToken,
                    TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds()
                });
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return BadRequest("Could not verify token and/or refresh token.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [AllowAnonymous]
        [HttpPost("resetrefreshtoken")]
        public IActionResult ResetRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            _logger.Information("Refresh token started.");
            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token);
                var username = principal.Claims.Where(x => x.Type.ToLower() == "email").FirstOrDefault().Value;
                
                var user = _userService.GetbyEmail(username);
                if(user == null || user.RefreshToken != refreshTokenRequest.RefreshToken)
                {
                    return BadRequest();
                }

                var newRefreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                _userService.UpdateUser(_mapper.Map<UserRequest>(user));

                return new ObjectResult(new
                {
                    user.RefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return BadRequest("Could not verify token and/or refresh token.");
        }
    }
}
