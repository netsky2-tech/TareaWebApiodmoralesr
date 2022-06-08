﻿using AWDAL.Admon;
using AWDAL.Production;
using AWEntities.Authentication;
using AWEntities.Production;
using AWEntities.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWBLL.Production
{
    public class AuthenticationBLL
    {
        private readonly AuthenticationDB dbCategory;

        public static Result Insert(user User)
        {
            return AuthenticationDB.Login(User);
        }

    }
}