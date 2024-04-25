using DevFreela.Core.Entities; // Importa o namespace contendo as entidades de usuário
using System.Threading.Tasks; // Importa o namespace para suporte à execução assíncrona

namespace DevFreela.Core.Repositories
{
    // Interface responsável pela definição de contrato para repositórios de usuários
    public interface IUserRepository
    {
        // Método assíncrono para obter um usuário pelo seu ID
        Task<User> GetByIdAsync(int id);

        // Método assíncrono para buscar um usuário por e-mail e hash de senha no banco de dados
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
    }
}
