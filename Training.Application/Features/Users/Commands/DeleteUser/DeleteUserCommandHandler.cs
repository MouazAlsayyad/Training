using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Contracts.Persistence;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand, BaseResponse<object>>
    {
        private readonly IAsyncRepository<User> _userRepository;

        public DeleteUserCommandHandler(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return new BaseResponse<object>("User not found", false, 404);
            }

            await _userRepository.DeleteAsync(user);

            return new BaseResponse<object>("User has been Deleted", true, 200);
        }
    }
}
