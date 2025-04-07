using ContactList.Application.Contracts.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Security
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public Task<string> HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return Task.FromResult(hashedPassword);
        }

        public Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return Task.FromResult(isValid);
        }
    }
}
