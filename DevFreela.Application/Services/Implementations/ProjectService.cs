/*

using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DevFreela.Application.Services.Implementations
{
    // Serviço responsável por operações relacionadas a projetos
   // public class ProjectService : IProjectService
  //  {
   //     private readonly DevFreelaDbContext _dbContext; // Contexto do banco de dados
   //     private readonly string _connectionString; // String de conexão com o banco de dados

        // Construtor do serviço, recebe o contexto do banco de dados e a configuração
  //      public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
      //  {
   //         _dbContext = dbContext; // Inicialização do contexto do banco de dados
   //         _connectionString = configuration.GetConnectionString("DevFreelaCs"); // Obtenção da string de conexão
   //     }



           // FOI SUBSTITUIDO PELO HANDLER = CreateProjectCommandHandler

        // Método para criar um novo projeto
   //     public int Create(NewProjectInputModel inputModel) 
   //     {

            // Criação de uma instância de projeto com os dados fornecidos
   //         var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);

   //         _dbContext.Projects.Add(project); // Adiciona o projeto ao contexto do banco de dados
   //         _dbContext.SaveChanges(); // Salva as alterações no banco de dados

    //        return project.Id; // Retorna o ID do projeto criado

    //    }




        // Método para adicionar um comentário a um projeto
    //    public void CreateComment(CreateCommentInputModel inputModel)
    //    {
            // Criação de um novo comentário com os dados fornecidos
    //        var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);

    //        _dbContext.ProjectComments.Add(comment); // Adiciona o comentário ao contexto do banco de dados
    //        _dbContext.SaveChanges(); // Salva as alterações no banco de dados
     //   }





        // Método para excluir um projeto
     //   public void Delete(int id)
      //  {
            // Busca o projeto pelo ID fornecido
      //      var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

      //      project.Cancel(); // Cancela o projeto
      //      _dbContext.SaveChanges(); // Salva as alterações no banco de dados
      //  }






        // Método para finalizar um projeto
    //    public void Finish(int id)
   //     {
            // Busca o projeto pelo ID fornecido
  //          var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

   //         project.Finish(); // Finaliza o projeto
   //         _dbContext.SaveChanges(); // Salva as alterações no banco de dados
   //     }






        // Método para obter todos os projetos
     //   public List<ProjectViewModel> GetAll(string query)
    //    {
            // Obtém todos os projetos do contexto do banco de dados
    //        var projects = _dbContext.Projects;

            // Mapeia os projetos para o modelo de visualização e os retorna
   //         var projectsViewModel = projects
    //            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt))
   //             .ToList();

  //          return projectsViewModel; // Retorna a lista de projetos
  //      }





        // Método para obter os detalhes de um projeto pelo ID
  //      public ProjectDetailsViewModel GetById(int id)
  //      {
            // Busca o projeto pelo ID fornecido, incluindo informações do cliente e do freelancer
  //          var project = _dbContext.Projects
  //              .Include(p => p.Client)
  //              .Include(p => p.Freelancer)
   //             .SingleOrDefault(p => p.Id == id);

   //         if (project == null) return null; // Retorna nulo se o projeto não for encontrado
   //
            // Cria um modelo de visualização dos detalhes do projeto com as informações obtidas
  //          var projectDetailsViewModel = new ProjectDetailsViewModel(
  //              project.Id,
   //             project.Title,
   //             project.Description,
    //            project.TotalCost,
    //            project.StartedAt,
    //            project.FinishedAt,
    //            project.Client.FullName,
    //            project.Freelancer.FullName
    //        );

    //        return projectDetailsViewModel; // Retorna o modelo de visualização dos detalhes do projeto
     //   }

        // Método para iniciar um projeto com o DAPPER
  //      public void Start(int id)
   //     {
            // Busca o projeto pelo ID fornecido
   //         var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

   //         project.Start(); // Inicia o projeto
   //         _dbContext.SaveChanges(); // Salva as alterações no banco de dados

            // Utiliza Dapper para executar uma query direta no banco de dados para atualizar o status do projeto
    //        using (var sqlConnection = new SqlConnection(_connectionString))
     //       {
      //          sqlConnection.Open(); // Abre a conexão com o banco de dados
     //
                // Query para atualizar o status e a data de início do projeto
      //          var script = "UPDATE Projects SET Status = @status, StartedAt = @startedat WHERE Id = @id";

                // Executa a query utilizando Dapper
       //         sqlConnection.Execute(script, new { status = project.Status, startedat = project.StartedAt, id });
      //      }
     //   }




        // Método para atualizar um projeto
    //    public void Update(UpdateProjectInputModel inputModel)
  //      {
            // Busca o projeto pelo ID fornecido
  //          var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

   //         if (project != null) // Verifica se o projeto foi encontrado
  //          {
   //             _dbContext.Projects.Attach(project); // Anexa o projeto ao contexto do banco de dados
  //
                // Atualiza as informações do projeto com os dados fornecidos
  //              project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
  //              _dbContext.SaveChanges(); // Salva as alterações no banco de dados
    //         }
 //       }
    }
//}


*/
