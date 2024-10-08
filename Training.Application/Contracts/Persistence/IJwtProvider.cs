using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entities;

namespace Training.Application.Contracts.Persistence
{
    public interface IJwtProvider
    {
        public string Generate(User user);
        public string GetUserIdFromToken(string token);
    }
}
