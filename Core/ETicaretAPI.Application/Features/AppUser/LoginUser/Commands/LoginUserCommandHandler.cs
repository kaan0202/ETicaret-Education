using ETicaretAPI.Application.Abstraction.Services;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Token;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.AppUser.LoginUser.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }
        async Task<LoginUserCommandResponse> IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>.Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 15);

            return new()
            {
                Token = token
            };
        }
    }
}

