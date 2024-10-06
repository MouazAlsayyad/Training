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

namespace Training.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, BaseResponse<object>>
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private readonly IAsyncRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IAsyncRepository<User> userRepository, IAsyncRepository<Role> roleRepository, IAsyncRepository<UserRole> userRole, IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRole;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = _mapper.Map<User>(request);
            
            var User = await _userRepository.AddAsync(userToAdd);

            if(request.Roles != null)
            {
                foreach(var roleId in request.Roles)
                {
                    var Role = await _roleRepository.GetByIdAsync(roleId);
                    
                    if(Role != null)
                    {
                        await _userRoleRepository.AddAsync(new UserRole()
                        {
                            RoleId = roleId,
                            UserId = User.UserId
                        });
                    }

                }
            }

            return new BaseResponse<object>("User has been Added", true, 200);
        }
    }
}
