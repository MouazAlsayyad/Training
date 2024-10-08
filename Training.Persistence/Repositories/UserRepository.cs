using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Contracts.Persistence;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserRepository(IAsyncRepository<User> userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<BaseResponse<string>> LogInAsync(User user)
        {
            var userToLogin = _userRepository.Where(u => u.Email == user.Email).FirstOrDefault();

            byte[] salt = new byte[16] { 41, 214, 78, 222, 28, 87, 170, 211, 217, 125, 200, 214, 185, 144, 44, 34 };

            string CheckPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            if (userToLogin == null)
            {
                return new BaseResponse<object>("Invalid email or password", false, 400);
            }

            if (CheckPassword == userToLogin.Password)
            {
                var token = _jwtProvider.Generate(userToLogin);

                return new BaseResponse<object>("Login Sucsses", true, 200, token);
            }

            return new BaseResponse<object>("Invalid email or password", false, 400);
        }
    }
}
