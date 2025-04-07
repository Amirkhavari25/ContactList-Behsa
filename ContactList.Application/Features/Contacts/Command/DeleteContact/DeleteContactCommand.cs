using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Command.DeleteContact
{
    public record DeleteContactCommand(Guid contactId , string creatorId)  : IRequest<ResultDTO<string>>;
    
}
