using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using MediatR; // Importa o MediatR para lidar com o padrão mediator
using System.Linq; // Importa para utilizar o método SingleOrDefault()
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.FinishProject
{
    // Classe responsável por manipular o comando de atualizar um projeto
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository; // Declaração do repositório de projetos

        // Construtor para inicializar o contexto do banco de dados
        public FinishProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o contexto do banco de dados à variável local
        }










        // Método para lidar com o comando de finalizar um projeto
        public async Task<Unit> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            // Busca o projeto pelo ID fornecido
            var project =  await _projectRepository.GetByIdAsync(request.Id);

           

            // Finaliza o projeto
            project.Finish();

            // Salva as alterações no banco de dados
            await _projectRepository.SaveChangesAsync();

            // Retorna uma unidade (representando uma operação bem-sucedida)
            return Unit.Value;
        }
    }
}
