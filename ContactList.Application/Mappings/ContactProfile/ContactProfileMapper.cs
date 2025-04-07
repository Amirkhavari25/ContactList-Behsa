using AutoMapper;
using ContactList.Application.DTOs;
using ContactList.Application.Features.Contacts.Command.CreateContact;
using ContactList.Domain.Entities;


namespace ContactList.Application.Mappings.ContactProfile
{
    public class ContactProfileMapper : Profile
    {
        public ContactProfileMapper()
        {
            CreateMap<Contact, ContactDTO>();
            CreateMap<CreateContactCommand, Contact>()
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore());
        }
    }
}
