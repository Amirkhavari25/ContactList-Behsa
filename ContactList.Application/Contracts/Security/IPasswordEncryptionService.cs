using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Security
{
    public interface IPasswordEncryptionService
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hashedPassword);
    }
}
