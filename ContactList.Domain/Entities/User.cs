using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(20)]
        [Phone]
        public string Mobile { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDelete { get; set; } = false;

        public ICollection<Contact> Contacts { get; set; }
    }
}
