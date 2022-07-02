﻿using Core.Common;
using Core.Repository;
using Core.RequestModels;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MemberRepository : BaseRepository<Members>, IMemberRepository
    {
        MCLDBContext _context;
        public MemberRepository(MCLDBContext context, IUserResolverService user) : base(context, user)
        {
            _context = context;
        }

        public Page<Members> Search(MemberSearchRequestModel searchModel, int skip, int take)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            IQueryable<Members> query = _context.Set<Members>();

            query = query.Where(x => (searchModel.MemberId == 0 || (x.Email.Length > 0 && x.Id == searchModel.MemberId))
                && (string.IsNullOrEmpty(searchModel.MemberName) || x.Name.Contains(searchModel.MemberName))
                && (searchModel.JoiningDate ==  null || x.JoiningDate == searchModel.JoiningDate)
                && (searchModel.DateOfBirth == null || x.DateOfBirth == searchModel.DateOfBirth)
                && (searchModel.NID == 0 || x.NID == searchModel.NID)
                && (string.IsNullOrEmpty(searchModel.Phone) || x.Phone.Contains(searchModel.Phone))
                && x.IsActive
                && x.IsVerified
            );

            return new Page<Members>
            {
                Data = query.OrderBy(a => a.Id).Skip(skip).Take(take)
                    .Include(x => x.Attachments).ThenInclude(x => x.AttachmentType)
                    .Include(x => x.Name)
                    .Include(x => x.NID)
                    .Include(x => x.Phone)
                    .Include(x => x.Email)
                    .Include(x => x.Gender)
                    .Include(x => x.MaritalStatus)
                    .Include(x => x.IsActive).ToList(),
                Total = query.Count()
            };
        }
    }
}
