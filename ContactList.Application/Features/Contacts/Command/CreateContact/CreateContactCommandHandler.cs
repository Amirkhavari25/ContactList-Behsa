using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using ContactList.Domain.Entities;
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
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ResultDTO<ContactDTO>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<ResultDTO<ContactDTO>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contactEntity = _mapper.Map<Contact>(request);
                contactEntity.CreateDate = DateTime.Now;
                contactEntity.UpdateDate = DateTime.Now;
                await _contactRepository.AddAsync(contactEntity);
                var contactDTO = _mapper.Map<ContactDTO>(contactEntity);
                return ResultDTO<ContactDTO>.SuccessResult(contactDTO);
            }
            catch (Exception ex)
            {
                return ResultDTO<ContactDTO>.FailureResult($"Error creating contact: {ex.Message}");
            }
        }
    }
}
