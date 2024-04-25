using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateUser
{
    // Implementa o IRequestHandler para o comando CreateUserCommand, que retorna um inteiro (o ID do usuário criado)
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IAuthService _authService;
        private readonly IProjectRepository _projectRepository; // Repositório de projetos

        // Construtor que recebe o repositório de projetos como dependência
        public CreateUserCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o repositório de projetos à variável local
        }






        // Método Handle para lidar com a execução do comando CreateUserCommand  - GUARDAR SENHA SEGURA.
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Calcula o hash da senha usando o serviço de autenticação
            var passwordHash = _authService.ComputeSha256Hash(request.passwordHash);

            // Cria uma nova instância de User com os dados fornecidos pelo comando
            var user = new User(request.FullName, request.Email, request.BirthDate, passwordHash, request.Role);

            // Adiciona o novo usuário ao contexto do banco de dados
            await _projectRepository.AddAsync(user);

            // Salva as alterações no banco de dados
            await _projectRepository.SaveChangesAsync();

            // Retorna o ID do usuário criado
            return user.Id;
        }
    }
}
