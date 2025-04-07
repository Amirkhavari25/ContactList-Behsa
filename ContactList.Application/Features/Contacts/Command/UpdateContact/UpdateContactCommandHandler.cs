using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Command.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ResultDTO<string>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<ResultDTO<string>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.creatorId, out var userId))
                {
                    return ResultDTO<string>.FailureResult("Invalid user ID.");
                }
                var contact = await _contactRepository.GetByIdAsync(request.Id, userId);
                if (contact == null)
                {
                    return ResultDTO<string>.FailureResult("Contact not found");
                }
                var updatedContact = _mapper.Map(request, contact);
                updatedContact.UpdateDate = DateTime.Now;

                await _contactRepository.UpdateAsync(updatedContact);
                return ResultDTO<string>.SuccessResult("Successfuly updated");

            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult(ex.Message);
            }
        }
    }
}
