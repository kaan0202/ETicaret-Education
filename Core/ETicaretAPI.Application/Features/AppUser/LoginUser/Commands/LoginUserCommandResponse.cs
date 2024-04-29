using ETicaretAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.LoginUser.Commands
{
    public class LoginUserCommandResponse
    {
        public DTOs.Token Token { get; set; }
        public string Message { get; set; }
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public DTOs.Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
