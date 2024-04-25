using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {

        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }





        // Método para lidar com a execução do comando LoginUserCommand     
        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Calcula o hash da senha fornecida usando o serviço de autenticação
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            // Busca o usuário no banco de dados pelo e-mail e pelo hash de senha
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            // Se o usuário não for encontrado, retorna null
            if (user == null)
            {
                return null;
            }

            // Gera um token JWT para o usuário encontrado usando o serviço de autenticação
            var token = _authService.GenerateJwtToken(user.Email, user.Role);

            // Retorna um objeto LoginUserViewModel contendo o e-mail do usuário e o token JWT gerado
            return new LoginUserViewModel(user.Email, token);
        }




    }
}
