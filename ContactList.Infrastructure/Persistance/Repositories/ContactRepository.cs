using ContactList.Application.Contracts.Persistance;
using ContactList.Domain.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Persistance.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DapperDbContext _dbContext;

        public ContactRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Contact contact)
        {
            const string query = @"
            INSERT INTO Contacts (Id, Name, PhoneNumber, Email, UserId, CreateDate, UpdateDate, IsDelete)
            VALUES (@Id, @Name, @PhoneNumber, @Email, @UserId, @CreateDate, @UpdateDate, @IsDelete)";

            var parameters = new
            {
                contact.Id,
                contact.Name,
                contact.PhoneNumber,
                contact.Email,
                contact.UserId,
                contact.CreateDate,
                contact.UpdateDate,
                contact.IsDelete
            };

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            const string query = @"
            UPDATE Contacts
            SET IsDelete = 1
            WHERE Id = @Id AND UserId = @UserId";

            var parameters = new { Id = id, UserId = userId };

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllByUserIdAsync(Guid userId)
        {
            const string query = @"
            SELECT * FROM Contacts
            WHERE UserId = @UserId AND IsDelete = 0";

            using (var connection = _dbContext.CreateConnection())
            {
                var contacts = await connection.QueryAsync<Contact>(query, new { UserId = userId });
                return contacts;
            }
        }

        public async Task<Contact?> GetByIdAsync(Guid id, Guid userId)
        {
            const string query = @"
            SELECT * FROM Contacts
            WHERE Id = @Id AND UserId = @UserId AND IsDelete = 0";

            using (var connection = _dbContext.CreateConnection())
            {
                var contact = await connection.QueryFirstOrDefaultAsync<Contact>(query, new { Id = id, UserId = userId });
                return contact;
            }
        }

        public async Task UpdateAsync(Contact contact)
        {
            const string query = @"
            UPDATE Contacts
            SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email, 
                UpdateDate = @UpdateDate
            WHERE Id = @Id AND UserId = @UserId AND IsDelete = 0";

            var parameters = new
            {
                contact.Id,
                contact.Name,
                contact.PhoneNumber,
                contact.Email,
                contact.UpdateDate,
                contact.UserId
            };

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

}
