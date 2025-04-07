using ContactList.Application.Contracts.Persistance;
using ContactList.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _context;
        public UserRepository(DapperDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            const string query = @"
            INSERT INTO Users (Id, Email, Username, Mobile, PasswordHash, IsDelete)
            VALUES (@Id, @Email, @Username, @Mobile, @PasswordHash, @IsDelete);";

            var parameters = new
            {
                user.Id,
                user.Email,
                user.Username,
                user.Mobile,
                user.PasswordHash,
                user.IsDelete,
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            const string query = @"SELECT * FROM Users WHERE Email = @Email AND IsDelete = 0;";


            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
                return user;
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string query = @"SELECT * FROM Users WHERE Id = @Id AND IsDelete = 0;";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
                return user;
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            const string query = @"SELECT * FROM Users WHERE Username = @Username AND IsDelete = 0;";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
                return user;
            }
        }
    }
}
