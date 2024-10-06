using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Contracts.Persistence;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, BaseResponse<object>>
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IAsyncRepository<User> userRepository, IAsyncRepository<UserRole> userRoleRepository, IAsyncRepository<Role> roleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(request.UserId);

            if(userToUpdate == null)
            {
                return new BaseResponse<object>("User not found", false, 404);
            }

            _mapper.Map(request, userToUpdate, typeof(UpdateUserCommand), typeof(User));

            if (request.Roles != null)
            {
                foreach (var roleId in request.Roles)
                {
                    var Role = await _roleRepository.GetByIdAsync(roleId);

                    if (Role != null)
                    {
                        var userRole =  _userRoleRepository.Where(u => u.RoleId == Role.RoleId && u.UserId == request.UserId).FirstOrDefault();
                        if(userRole == null)
                        {
                            await _userRoleRepository.AddAsync(new UserRole()
                            {
                                RoleId = roleId,
                                UserId = request.UserId
                            });
                        }
                    }
                }
            }
            return new BaseResponse<object>("User has been Updated", true, 200);
        }
    }
}
