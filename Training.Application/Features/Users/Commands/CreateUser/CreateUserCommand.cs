﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Responses;

namespace Training.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponse<object>>
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<int>? Roles { get; set; }
    }
}
