using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Command.CreateContact
{
    public class CreateContactCommand : IRequest<ResultDTO<ContactDTO>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; } 

        [Required]
        public string Email { get; set; } 

        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
