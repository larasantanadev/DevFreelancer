using DevFreela.Core.Entities;               // Importa as entidades do projeto
using DevFreela.Core.Repositories;
using MediatR;                               // Importa o MediatR para lidar com o padrão mediator
using System.Threading;                      // Importa para lidar com threads
using System.Threading.Tasks;                // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.CreateProject
{
    // Classe que manipula o comando de criação de projeto
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectRepository _projectRepository; // Repositório de projetos

        // Construtor que recebe o repositório de projetos como dependência
        public CreateProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o repositório de projetos à variável local
        }







        // Método que manipula a execução do comando de criação de projeto
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // Cria uma instância de projeto com os dados fornecidos pelo comando
            var project = new Project(request.Title, request.Description, request.IdClient,
                request.IdFreelancer, request.TotalCost);

            // Adiciona o projeto ao repositório
            await _projectRepository.AddAsync(project);

            // Retorna o ID do projeto criado
            return project.Id;
        }
    }
}


// INÍCIO:
// VERIFICAR AS DEPENDÊNCIAS QUE ESTÃO NO SERVICES.

// NOTAS:
// ASYNC - ASSÍNCRONO || DEVE-SE UTILIZAR A PALAVRA-CHAVE: AWAIT
// O HANDLER É MAIS ORGANIZADO DO QUE O SERVICES, POIS SEPARA CADA FUNCIONALIDADE.
// SUBSTITUI O SERVICES, INPUTMODEL...
