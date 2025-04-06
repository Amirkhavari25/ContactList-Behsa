using ContactList.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Email { get; set; } = default!;

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
