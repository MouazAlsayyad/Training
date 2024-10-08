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

namespace Training.Application.Features.Authentication.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, BaseResponse<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAsyncRepository<UserToken> _userTokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IMapper mapper, IUserRepository userRepository, IAsyncRepository<UserToken> userTokenRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _jwtProvider = jwtProvider;
            _mapper = mapper;
        }

        public async Task<BaseResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var response = await _userRepository.LogInAsync(user);

            if (response.Success)
            {
                await _userTokenRepository.AddAsync(new UserToken()
                {
                    Token = response.Data!,
                    UserId = user.UserId
                });
            }

            return response;


        }
    }
}
