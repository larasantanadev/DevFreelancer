using System;
using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    // Representa a entidade de usuário na aplicação
    public class User : BaseEntity
    {
        // Construtor da classe User, utilizado para criar uma nova instância de usuário
        public User(string fullName, string email, DateTime birthDate, string password, string role)
        {
            // Atribui os valores passados como parâmetros às propriedades correspondentes
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now; // Define a data de criação como a data e hora atual
            Active = true; // Define o usuário como ativo por padrão
            Password = password;
            Role = role;




            // Inicializa as listas de habilidades, projetos próprios, projetos freelancers e comentários
            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();
            Comments = new List<ProjectComment>();
        }




        // Propriedades da entidade User
        public string FullName { get; private set; } // Nome completo do usuário
        public string Email { get; private set; } // E-mail do usuário
        public DateTime BirthDate { get; private set; } // Data de nascimento do usuário
        public DateTime CreatedAt { get; private set; } // Data de criação do usuário
        public bool Active { get; set; } // Indica se o usuário está ativo ou não




        // Propriedades adicionais relacionadas à autenticação e autorização
        public string Password { get; private set; } // Senha do usuário
        public string Role { get; private set; } // Papel (role) do usuário




        // Listas de relacionamentos com outras entidades
        public List<UserSkill> Skills { get; private set; } // Lista de habilidades do usuário
        public List<Project> OwnedProjects { get; private set; } // Lista de projetos próprios do usuário
        public List<Project> FreelanceProjects { get; set; } // Lista de projetos freelancers do usuário
        public List<ProjectComment> Comments { get; private set; } // Lista de comentários feitos pelo usuário em projetos
    }
}
