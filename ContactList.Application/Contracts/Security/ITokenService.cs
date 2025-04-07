using ContactList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Security
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
