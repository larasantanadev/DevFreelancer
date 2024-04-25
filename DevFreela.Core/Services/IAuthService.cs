using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Services
{
    // Definindo uma interface chamada IAuthService
    public interface IAuthService
    {
        // Método para gerar um token JWT com base no e-mail e na função (ou papel) do usuário
        string GenerateJwtToken(string email, string role);

        // Método para calcular o hash SHA256 de uma senha
        string ComputeSha256Hash(string password);
    }
}
