using ContactList.Application.Contracts.Persistance;
using ContactList.Application.Contracts.Security;
using ContactList.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Features.Users.Commands.LoginUser
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultDTO<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly ITokenService _tokenService;
        public LoginCommandHandler(IUserRepository userRepository,IPasswordEncryptionService passwordEncryptionService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
            _tokenService = tokenService;
        }
        public async Task<ResultDTO<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.email);
            if (user == null)
            {
                return ResultDTO<string>.FailureResult("User not found");
            }
            //check password
            if (!await _passwordEncryptionService.VerifyPassword(request.password, user.PasswordHash))
            {
                return ResultDTO<string>.FailureResult("Wrong password or email");
            }
            try
            {
                // create token and return it
                var token = await _tokenService.CreateToken(user);
                return ResultDTO<string>.SuccessResult(token);
            }
            catch (Exception ex)
            {
                return ResultDTO<string>.FailureResult($"login error : {ex.Message}");
            }
        }
    }
}
