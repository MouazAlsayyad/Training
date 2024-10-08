using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Responses;

namespace Training.Application.Features.Authentication.Login
{
    public class LoginCommand: IRequest<BaseResponse<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
