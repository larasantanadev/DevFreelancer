using Azure.Core;
using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    /// Repositório para manipulação de entidades Project.
   

    // Migrando as dependências das queries e commands para o repositório.
  
    public class ProjectRepository : IProjectRepository  //Camada core.
    {
        private readonly DevFreelaDbContext _dbContext;   // QUERYS ALLPROJECTS                                                       
        private readonly string _connectionString;       //QUERY ALLSKILLS

        /// Construtor da classe ProjectRepository.
        public ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");      // DAPPER
        }

       







        // 1 PASSO PARA REFATORAÇÃO: EXTRAIR CLASSE
        /// Obtém todos os projetos existentes no banco de dados.      
        public async Task<List<Project>> GetAllAsync()
        {
            // Retorna uma lista assíncrona de todos os projetos do banco de dados.
            return await _dbContext.Projects.ToListAsync();
        }




        public async Task<Project> GetDetailsByIdAsync(int id)
        {
            return await _dbContext.Projects
               .Include(p => p.Client)
               .Include(p => p.Freelancer)
               .SingleOrDefaultAsync(p => p.Id == id);
        }









       





        public async Task AddAsync(Project project)
        {
            // Adiciona o projeto ao contexto do banco de dados
            await _dbContext.Projects.AddAsync(project);

            // Salva as alterações no banco de dados
            await _dbContext.SaveChangesAsync();
        }










        public async Task StartAsync(Project project)
        {
           

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                var script = "UPDATE Projects SET Status = @status, StartedAt = @started WHERE Id = @id";

                await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedat = project.StartedAt, project.Id });
            }
        }






        public async Task SaveChangesAsync()    // USEI NO UPDATE, DELETE, FINISH
        {
            await _dbContext.SaveChangesAsync();
        }






        // 1 PASSO PARA REFATORAÇÃO: EXTRAIR CLASSE
        public async Task<Project> GetByIdAsync(int id)
        {
            return await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }








        public async Task AddCommentAsync(ProjectComment projectComment)
        {
            await _dbContext.ProjectComments.AddAsync(projectComment);
            await _dbContext.SaveChangesAsync();
        }

       
    }
}






// CLASSE FEITA PARA REALIZAR A REFATORAÇÃO! 
// ACESSO A DADOS.

// PADRÃO REPOSITORY ELE TRATA COM ENTIDADES DO DOMINIO
// NÃO IMPEDE CONSULTA COM O DAPPER, DDO

