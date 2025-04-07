using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using MediatR;


namespace ContactList.Application.Features.Contacts.Command.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ResultDTO<string>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        public DeleteContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task<ResultDTO<string>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!Guid.TryParse(request.creatorId, out var userId))
                {
                    return ResultDTO<string>.FailureResult("Invalid user ID.");
                }
                var contact =await _contactRepository.GetByIdAsync(request.contactId,userId);
                if (contact == null)
                {
                    return ResultDTO<string>.FailureResult("Contact not found");
                }
                await _contactRepository.DeleteAsync(request.contactId,userId);
                return ResultDTO<string>.SuccessResult("Contact deleted successful");
            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult(ex.Message);
            }
        }
    }
}
