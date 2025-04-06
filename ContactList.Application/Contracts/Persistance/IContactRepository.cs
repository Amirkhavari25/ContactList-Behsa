using ContactList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Contracts.Persistance
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllByUserIdAsync(Guid userId);
        Task<Contact?> GetByIdAsync(Guid id, Guid userId);
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(Guid id, Guid userId);
    }
}
