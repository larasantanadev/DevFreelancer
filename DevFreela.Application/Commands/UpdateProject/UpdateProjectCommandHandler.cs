using DevFreela.Application.Commands.StartProject; // Importa o comando para iniciar um projeto
using DevFreela.Core.Repositories; // Importa os repositórios da aplicação
using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using MediatR; // Importa o MediatR para lidar com o padrão mediator
using System.Linq; // Importa o namespace System.Linq para operações de consulta
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.UpdateProject
{
    // Classe responsável por manipular o comando de atualizar um projeto
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository; // Declaração do repositório de projetos

        // Construtor para inicializar o contexto do banco de dados
        public UpdateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o contexto do banco de dados à variável local
        }














        // Método para lidar com a atualização de um projeto
        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            // Busca o projeto pelo ID fornecido
            var project = await _projectRepository.GetByIdAsync(request.Id);

            // Atualiza as informações do projeto com os dados fornecidos
            project.Update(request.Title, request.Description, request.TotalCost);

            await _projectRepository.SaveChangesAsync(); // Salva as alterações no banco de dados

            return Unit.Value; // Retorna uma instância de Unit (representando um resultado vazio)
        }
    }
}
