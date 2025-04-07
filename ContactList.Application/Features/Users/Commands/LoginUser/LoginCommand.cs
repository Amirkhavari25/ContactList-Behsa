using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Users.Commands.LoginUser
{
    public record LoginCommand(string email,string password) : IRequest<ResultDTO<string>>;
    
}
