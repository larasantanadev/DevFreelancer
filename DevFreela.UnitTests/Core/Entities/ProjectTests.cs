using DevFreela.Core.Entities; 
using DevFreela.Core.Enums; 


namespace DevFreela.UnitTests.Core.Entities 
{
    public class ProjectTests 
    {
        [Fact] // Atributo de marcação de teste. Indica que o método abaixo é um teste unitário.
        public void TestIfProjectStartWorks() // Define o método para start do projeto
        {
            // Cria uma instância de Project com dados de exemplo
            var project = new Project("nome de teste", "descrição de teste", 1, 2, 10000);


            // Verifica se o status do projeto é criado
            Assert.Equal(ProjectStatusEnum.Created, project.Status);



            // Verifica se a data de início do projeto é nula
            Assert.Null(project.StartedAt);



            // Verifica se o título do projeto não é nulo
            Assert.NotNull(project.Title);



            // Verifica se o título do projeto não está vazio
            Assert.NotEmpty(project.Title);



            // Verifica se a descrição do projeto não é nula
            Assert.NotNull(project.Description);



            // Verifica se a descrição do projeto não está vazia
            Assert.NotEmpty(project.Description);



            // Inicia o projeto
            project.Start();



            // Verifica se o status do projeto mudou para InProgress
            Assert.Equal(ProjectStatusEnum.InProgress, project.Status);



            // Verifica se a data de início do projeto não é nula após iniciar
            Assert.NotNull(project.StartedAt);
        }








        [Fact] // Atributo de marcação de teste. Indica que o método abaixo é um teste.
        public void TestIfProjectCompleteWorks() // Define o método de teste TestIfProjectCompleteWorks
        {
            // Cria uma instância de Project com dados de exemplo
            var project = new Project("nome de teste", "descrição de teste", 1, 2, 10000);

            // Inicia o projeto
            project.Start();
            // Completa o projeto
            project.Finish();

            // Verifica se o status do projeto mudou para Finished
            Assert.Equal(ProjectStatusEnum.Finished, project.Status);
            // Verifica se a data de conclusão do projeto não é nula após completar
            Assert.NotNull(project.FinishedAt);
        }








    }
}


// executar testes.