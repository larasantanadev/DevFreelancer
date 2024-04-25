using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using MediatR; // Importa o MediatR para lidar com padrão mediator
using System.Linq; // Importa para uso de LINQ
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.DeleteProject
{



    // Classe responsável por manipular o comando de atualizar um projeto
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository; // Declaração do repositório de projetos

        // Construtor para inicializar o contexto do banco de dados
        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o contexto do banco de dados à variável local
        }







        // Método para lidar com a exclusão de um projeto
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            // Busca o projeto pelo ID fornecido
            var project = await _projectRepository.GetByIdAsync(request.Id);

            project.Cancel(); // Cancela o projeto

            // Salva as alterações no banco de dados
            await _projectRepository.SaveChangesAsync();

            return Unit.Value; // Retorna Unit.Value para indicar que o comando foi executado com sucesso
        }
    }
}
