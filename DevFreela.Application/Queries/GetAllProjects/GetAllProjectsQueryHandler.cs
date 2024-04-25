using DevFreela.Application.ViewModels; // Importa os modelos de visualização
using DevFreela.Core.Repositories;
using MediatR; // Importa o MediatR para lidar com padrão mediator
using System.Collections.Generic; // Importa o namespace para trabalhar com coleções genéricas
using System.Linq; // Importa o namespace para trabalhar com consultas LINQ
using System.Threading; // Importa o namespace para trabalhar com operações assíncronas
using System.Threading.Tasks; // Importa o namespace para trabalhar com tarefas assíncronas

namespace DevFreela.Application.Queries.GetAllProjects
{
    // Manipulador de consulta para obter todos os projetos
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectViewModel>>
    {


        private readonly IProjectRepository _projectRepository;  //REFATORAÇÃO

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }





        // PASSO 3 PARA A REFATORAÇÃO: SUBSTITUIÇÃO PELO REPOSITÓRIO
        // Manipula a solicitação
        public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {


            // Obtém todos os projetos
            var projects = await _projectRepository.GetAllAsync();

            // Mapeia os projetos para seus respectivos ViewModels
            var projectsViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
                .ToList();

            // Retorna a lista de ViewModels dos projetos
            return projectsViewModel;


           
        }
    }
}



// PARA TESTE UNITÁRIO É ESSENCIAL DESACLOPAR NO BANCO DE DADOS.



// CÓDIGO ANTERIOR

// Método para lidar com a consulta para obter todos os projetos
//         public async Task<List<ProjectViewModel>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
//      {
// Obtém todos os projetos do contexto do banco de dados
//          var projects = _dbContext.Projects;

// Mapeia os projetos para o modelo de visualização e os retorna
//          var projectsViewModel = await projects
//               .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)) // Mapeia cada projeto para o modelo de visualização
//              .ToListAsync(); // Converte o resultado para uma lista assíncrona

//          return projectsViewModel; // Retorna a lista de projetos




//     private readonly DevFreelaDbContext _dbContext; // Contexto do banco de dados

// Construtor que inicializa o manipulador com o contexto do banco de dados
//      public GetAllProjectsQueryHandler(DevFreelaDbContext dbContext)
//      {
//          _dbContext = dbContext; // Atribui o contexto do banco de dados à variável local
//      }
