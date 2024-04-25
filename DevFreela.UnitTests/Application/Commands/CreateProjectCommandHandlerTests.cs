using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;


namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {



            // ARRANGE

            // Aqui estamos configurando o ambiente para o teste.
            // Criamos substitutos (mocks) para as dependências da classe que estamos testando.

            // Criamos um substituto para o repositório de projetos.
            var projectRepository = Substitute.For<IProjectRepository>();

            // Criamos um objeto de comando de criação de projeto com dados simulados.
            var createProjectCommand = new CreateProjectCommand
            {
                Title = "testeee",
                Description = "testteteteteettet",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 100
            };

            // Configuramos o comportamento do método AddAsync do repositório de projetos.
            // Queremos que ele retorne um id de projeto simulado (no caso, 1) quando for chamado com qualquer argumento.
            projectRepository.AddAsync(Arg.Any<Project>())
                .ReturnsForAnyArgs(Task.FromResult(1)); // Retorna um id de projeto simulado encapsulado em uma tarefa

            // Criamos uma instância do manipulador de comando que estamos testando (CreateProjectCommandHandler).
            // Passamos o substituto do repositório de projetos como dependência.
            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository);




            // ACT

            // Aqui estamos executando o teste propriamente dito.
            // Chamamos o método Handle do manipulador de comando para processar o comando de criação de projeto.
            // Isso deve retornar um id de projeto.

            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());




            // ASSERT

            // Aqui estamos verificando se o comportamento do sistema é conforme o esperado.

            // Verificamos se o id retornado é maior ou igual a zero, o que indica que o projeto foi criado com sucesso.
            Assert.True(id >= 0);

            // Verificamos se o método AddAsync do repositório de projetos foi chamado exatamente uma vez,
            // com qualquer objeto Project como argumento.
            // Isso garante que o projeto tenha sido adicionado ao repositório durante o teste.
            await projectRepository.Received(1).AddAsync(Arg.Any<Project>());
        }
    }
}
