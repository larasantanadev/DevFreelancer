using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDetailsViewModel>
    {
        private readonly IProjectRepository _projectRepository;  //REFATORAÇÃO

        public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }




        public async Task<ProjectDetailsViewModel> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            // Busca o projeto pelo ID fornecido
            var project = await _projectRepository.GetByIdAsync(request.Id);

            if (project == null)
            {
                return null; // Retorna nulo se o projeto não for encontrado
            }

            // Cria um modelo de visualização dos detalhes do projeto com as informações obtidas
            var projectDetailsViewModel = new ProjectDetailsViewModel(
                 project.Id,
                 project.Title,
                 project.Description,
                 project.TotalCost,
                 project.StartedAt,
                 project.FinishedAt,
                 project.Client.FullName,
                 project.Freelancer.FullName
                 );

            return projectDetailsViewModel; // Retorna o modelo de visualização dos detalhes do projeto
        }
    }
}
