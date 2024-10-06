using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Responses;

namespace Training.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponse<object>>
    {
        public int UserId { get; set; }
    }
}
