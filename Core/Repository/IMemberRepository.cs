﻿using Core.RequestModels;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IMemberRepository : IBaseRepository<Member>
    {
        Page<Member> Search(MemberSearchRequest searchModel, int skip, int take);
        bool IsExistingMember(string email, int code, string phone, long nid);
    }
}
