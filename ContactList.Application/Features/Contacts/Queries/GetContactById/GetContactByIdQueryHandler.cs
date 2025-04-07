using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Contacts.Queries.GetContactById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ResultDTO<ContactDTO>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetContactByIdQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<ResultDTO<ContactDTO>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.creatorId, out var userId))
                {
                    return ResultDTO<ContactDTO>.FailureResult("Invalid user ID.");
                }
                var contact = await _contactRepository.GetByIdAsync(request.contactId, userId);
                var result = _mapper.Map<ContactDTO>(contact);
                return ResultDTO<ContactDTO>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                return ResultDTO<ContactDTO>.FailureResult($"Error getting contact detail:{ex.Message}");
            }
        }
    }
}
