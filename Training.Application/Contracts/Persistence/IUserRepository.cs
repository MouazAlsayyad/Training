using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<BaseResponse<string>> LogInAsync(User user);
    }
}
