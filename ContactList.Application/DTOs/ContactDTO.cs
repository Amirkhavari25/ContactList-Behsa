using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.DTOs
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
