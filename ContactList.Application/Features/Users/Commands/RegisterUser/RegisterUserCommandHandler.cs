using AutoMapper;
using ContactList.Application.Contracts.Persistance;
using ContactList.Application.DTOs;
using ContactList.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO<UserDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ResultDTO<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //check if user already exist
                var checkUserExist = await _userRepository.GetByEmailAsync(request.Email);
                if (checkUserExist != null)
                {
                    return ResultDTO<UserDTO>.FailureResult("User already exist try to login");
                }
                //map to entity type
                var UserEntity = _mapper.Map<User>(request);
                //ToDo: hash password
                //save user in database
                //return success result
            }
            catch (Exception ex)
            {
                //better to log exception 
                return ResultDTO<UserDTO>.FailureResult($"Error user registration: {ex.Message}");
            }
        }
    }
}
