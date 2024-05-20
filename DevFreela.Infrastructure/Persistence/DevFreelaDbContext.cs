using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DevFreela.Infrastructure.Persistence
{
    // Definição da classe DevFreelaDbContext, que herda de DbContext
    public class DevFreelaDbContext : DbContext
    {
        // Construtor da classe que recebe opções de configuração do DbContext
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options)
        {

        }

        // Define propriedades DbSet para cada entidade do contexto

        // Representa a tabela de projetos no banco de dados
        public DbSet<Project> Projects { get; set; }

        // Representa a tabela de usuários no banco de dados
        public DbSet<User> Users { get; set; }

        // Representa a tabela de habilidades no banco de dados
        public DbSet<Skill> Skills { get; set; }

        // Representa a tabela de habilidades dos usuários no banco de dados
        public DbSet<UserSkill> UserSkills { get; set; }

        // Representa a tabela de comentários de projetos no banco de dados
        public DbSet<ProjectComment> ProjectComments { get; set; }

        // Método chamado durante o processo de construção do modelo do banco de dados
        // Aplica todas as configurações de entidades definidas no assembly atual
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}




//  Add-Migration NomeDaSuaMigration
//  Update-Database