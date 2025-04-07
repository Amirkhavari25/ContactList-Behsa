using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using MediatR;

namespace ContactList.Application.Features.Contacts.Queries.GettAllContacts
{
    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, ResultDTO<List<ContactDTO>>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public GetAllContactsQueryHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<ResultDTO<List<ContactDTO>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.creatorId, out var userId))
                {
                    return ResultDTO<List<ContactDTO>>.FailureResult("Invalid user ID.");
                }
                var contacts = await _contactRepository.GetAllByUserIdAsync(userId);
                var contactDTO = _mapper.Map<List<ContactDTO>>(contacts);

                return ResultDTO<List<ContactDTO>>.SuccessResult(contactDTO);
            }
            catch (Exception ex)
            {
                return ResultDTO<List<ContactDTO>>.FailureResult($"Error getting all tasks: {ex.Message}");
            }
        }
    }
}
