using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Command.UpdateContact
{
    public class UpdateContactCommand: IRequest<ResultDTO<string>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        [JsonIgnore]
        public string? creatorId { get; set; }
    }
}
