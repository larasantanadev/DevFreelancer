using Dapper; // Importa Dapper para mapeamento objeto-relacional
using DevFreela.Core.DTOs; // Importa os DTOs de Skill
using DevFreela.Core.Repositories; // Importa a interface do repositório de habilidades
using Microsoft.Data.SqlClient; // Importa para utilizar o SqlConnection
using Microsoft.Extensions.Configuration; // Importa para acessar a configuração do aplicativo
using System.Collections.Generic; // Importa para utilizar List<>
using System.Linq; // Importa para utilizar LINQ
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    // Implementação do repositório de habilidades utilizando Dapper
    public class SkillRepository : ISkillRepository
    {
        // String de conexão com o banco de dados
        private readonly string _connectionString;

        // Construtor que recebe a configuração do aplicativo para acessar a string de conexão
        public SkillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs"); // DAPPER
        }






        // Método para obter todas as habilidades
        public async Task<List<SkillDTO>> GetAllAsync()
        {
            // Inicializa uma nova conexão com o banco de dados
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                // Abre a conexão com o banco de dados
                sqlConnection.Open();

                // Consulta SQL para selecionar todas as habilidades do banco de dados
                var script = "SELECT Id, Description FROM Skills";

                // Executa a consulta SQL usando o Dapper e retorna uma lista de objetos SkillDTO
                var skills = await sqlConnection.QueryAsync<SkillDTO>(script);

                // Converte o resultado em uma lista e retorna
                return skills.ToList();
            }
        }
    }
}




//FEITO COM ENTITY

//var skills = _dbContext.Skills;

//var skillsViewModel = skills
//    .Select(s => new SkillViewModel(s.Id, s.Description))
//    .ToList();

//return skillsViewModel;





//Instalar Dapper na INFRASTRUCTURE