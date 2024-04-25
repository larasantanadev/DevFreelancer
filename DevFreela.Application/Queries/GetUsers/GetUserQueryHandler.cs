using DevFreela.Application.ViewModels;   // Importa o namespace contendo os ViewModels
using DevFreela.Core.Repositories;
using MediatR;   // Importa o namespace do MediatR
using System.Threading;   // Importa o namespace para suporte à execução assíncrona
using System.Threading.Tasks;   // Importa o namespace para suporte à execução assíncrona

namespace DevFreela.Application.Queries.GetUsers   // Namespace que contém a classe do manipulador de consulta
{
    // Classe responsável por manipular a consulta para obter um usuário por ID
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUserRepository _userRepository;  // Repositório de usuários

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }






        // Método que manipula a execução da consulta para obter um usuário por ID
        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            // Busca o usuário no banco de dados com base no ID fornecido na consulta
            var user = await _userRepository.GetByIdAsync(request.Id);

            // Se o usuário não for encontrado, retorna null
            if (user == null)
            {
                return null;
            }

            // Cria um ViewModel do usuário com os dados encontrados
            return new UserViewModel(user.FullName, user.Email);
        }
    }
}
