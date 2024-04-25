using DevFreela.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{

    // 2 PASSO PARA REFATORAÇÃO: EXTRAIR INFERFACE
    // Interface para o repositório de projetos.
    public interface IProjectRepository
    {
        // Obtém todos os projetos.
        Task<List<Project>> GetAllAsync();
        Task<Project> GetDetailsByIdAsync(int id);
        Task<Project> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task StartAsync(Project project);
        Task AddCommentAsync(ProjectComment projectComment);
        Task SaveChangesAsync();
        Task AddAsync(User user);
    }
}







// CLASSE PARA REALIZAR A REFATORAÇÃO TAMBÉM.
// AO UTILIZAR A INTERFACE, VOCÊ CONSEGUE O DESACOPLAMENTO COM O BANCO DE DADOS.
