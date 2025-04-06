using ContactList.Application.Contracts.Persistance;
using ContactList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Persistance.Repositories
{
    public class ContactRepository : IContactRepository
    {
        public Task AddAsync(Contact contact)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAllByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Contact?> GetByIdAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}
