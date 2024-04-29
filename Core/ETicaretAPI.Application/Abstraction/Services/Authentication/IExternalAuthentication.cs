﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstraction.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken,int tokenLifeTime);
        Task<DTOs.Token> GoogleLoginAsync(string idToken,int tokenLifeTime);
      
    }
}
