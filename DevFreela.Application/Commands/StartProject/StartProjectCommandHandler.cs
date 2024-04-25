using DevFreela.Core.Repositories; // Importa os repositórios da aplicação
using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using MediatR; // Importa o MediatR para lidar com o padrão mediator
using Microsoft.Data.SqlClient; // Importa o SQL Client para interação com o banco de dados
using Microsoft.EntityFrameworkCore; // Importa o Entity Framework Core para interação com o banco de dados
using System; // Importa o namespace System para funcionalidades básicas
using System.Linq; // Importa o namespace System.Linq para operações de consulta
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.StartProject
{
    // Classe responsável por manipular o comando de iniciar um projeto
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository; // Declaração do repositório de projetos

        // Construtor para inicializar o contexto do banco de dados
        public StartProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o contexto do banco de dados à variável local
        }














        // Método Handle para manipular o comando de iniciar um projeto
        public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            // Busca o projeto pelo ID fornecido
            var project = await _projectRepository.GetByIdAsync(request.Id);

            // Inicia o projeto
            project.Start();

            // Atualiza o projeto no banco de dados
            await _projectRepository.StartAsync(project);

            return Unit.Value; // Retorna uma instância de Unit (representando um resultado vazio)
        }
    }
}
