using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.ViewModels
{
    // ViewModel para representar os dados de login de um usuário
    public class LoginUserViewModel
    {
        // Construtor para inicializar a ViewModel com o e-mail e o token
        public LoginUserViewModel(string email, string token)
        {
            Email = email;
            Token = token;
        }

        // Propriedade para armazenar o e-mail do usuário
        public string Email { get; set; }

        // Propriedade para armazenar o token de autenticação do usuário
        public string Token { get; set; }
    }
}
