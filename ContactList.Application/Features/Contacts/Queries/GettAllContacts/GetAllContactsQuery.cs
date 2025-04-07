using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Queries.GettAllContacts
{
    public record GetAllContactsQuery(string creatorId) : IRequest<ResultDTO<List<ContactDTO>>>;
    
}
