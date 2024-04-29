using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.RefreshTokenLogin.Commands
{
    public class RefreshTokenLoginCommandResponse
    {
        public DTOs.Token Token { get; set; }
    }
}
