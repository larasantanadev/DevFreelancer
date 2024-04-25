using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsCommandHandlerTests    // TESTE UNITÁRIO
    {
      
        /// Testa se o manipulador GetAllProjectsQueryHandler retorna os projetos corretamente.
      
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnProjectViewModels()
        {
            //Padrão AAA


            // ARRANGE

            var projects = new List<Project>
            {
                new Project("nome1", "descrição1", 1, 2, 10),
                new Project("nome2", "descrição2", 1, 2, 20),
                new Project("nome3", "descrição3", 1, 2, 30)
            };

            // Cria uma substituição para o repositório de projetos
            var projectRepository = Substitute.For<IProjectRepository>();

            // Configura o comportamento da substituição para retornar a lista de projetos quando GetAllAsync() é chamado
            projectRepository.GetAllAsync().Returns(projects);

            // Cria uma instância do comando GetAllProjectsQuery
            var getAllProjectsQuery = new GetAllProjectsQuery("");

            // Cria uma instância do manipulador GetAllProjectsQueryHandler, passando o repositório de projetos como dependência
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepository);

            // ACT

            // Chama o método Handle do manipulador, que deve retornar uma lista de ProjectViewModels
            var projectViewModels = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // ASSERT

            // Asserções para verificar se os objetos retornados são os esperados
            Assert.NotNull(projectViewModels);
            Assert.NotEmpty(projectViewModels);
            Assert.Equal(projects.Count, projectViewModels.Count);

            // Verifica se o método GetAllAsync foi chamado exatamente uma vez
            await projectRepository.Received(1).GetAllAsync();
        }
    }
}
