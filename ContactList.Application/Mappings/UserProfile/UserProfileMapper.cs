using AutoMapper;
using ContactList.Application.DTOs;
using ContactList.Application.Features.Users.Commands.RegisterUser;
using ContactList.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Mappings.UserProfile
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<User, UserDTO>();


        }
    }
}
