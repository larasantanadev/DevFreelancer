using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext; // Contexto do banco de dados para acessar e manipular os dados

        // Construtor da classe UserRepository. Recebe o contexto do banco de dados e uma instância de IConfiguration para injeção de dependência.
        public UserRepository(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext; // Inicializa o contexto do banco de dados para uso posterior.
        }






        // Implementação do método GetByIdAsync da interface IUserRepository. Retorna um usuário com base no ID fornecido.
        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id); // Realiza uma consulta assíncrona no banco de dados para encontrar o usuário com o ID especificado.
        }




        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            // Retorna o usuário com o e-mail e o hash de senha correspondentes, ou null se não encontrado
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }



    }
}
