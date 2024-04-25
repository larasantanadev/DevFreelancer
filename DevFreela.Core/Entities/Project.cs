using DevFreela.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevFreela.Core.Entities
{
    // Classe que representa um projeto no sistema
    public class Project : BaseEntity
    {
        // Construtor da classe Project, utilizado para inicializar as propriedades
        public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            // Validações para garantir que os parâmetros estejam corretos
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("O título do projeto não pode ser nulo ou vazio.", nameof(title));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("A descrição do projeto não pode ser nula ou vazia.", nameof(description));
            }

            if (totalCost < 0)
            {
                throw new ArgumentException("O custo total do projeto não pode ser negativo.", nameof(totalCost));
            }

            // Inicialização das propriedades com os valores fornecidos
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;

            // Inicialização de outras propriedades
            CreatedAt = DateTime.Now;
            Status = ProjectStatusEnum.Created;
            Comments = new List<ProjectComment>();
        }

        // Título do projeto
        public string Title { get; private set; }

        // Descrição do projeto
        public string Description { get; private set; }

        // ID do cliente relacionado ao projeto
        public int IdClient { get; private set; }

        // Cliente relacionado ao projeto
        public User Client { get; private set; }

        // ID do freelancer relacionado ao projeto
        public int IdFreelancer { get; private set; }

        // Freelancer relacionado ao projeto
        public User Freelancer { get; private set; }

        // Custo total do projeto, com configuração de tipo de coluna SQL
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalCost { get; private set; }

        // Data de criação do projeto
        public DateTime CreatedAt { get; private set; }

        // Data de início do projeto (opcional)
        public DateTime? StartedAt { get; private set; }

        // Data de término do projeto (opcional)
        public DateTime? FinishedAt { get; private set; }

        // Status do projeto
        public ProjectStatusEnum Status { get; private set; }

        // Comentários relacionados ao projeto
        public List<ProjectComment> Comments { get; private set; }

        // Método para cancelar o projeto
        public void Cancel()
        {
            if (Status == ProjectStatusEnum.InProgress || Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.Cancelled;
            }
        }

        // Método para iniciar o projeto
        public void Start()
        {
            if (Status == ProjectStatusEnum.Created)
            {
                Status = ProjectStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        // Método para finalizar o projeto
        public void Finish()
        {
            if (Status == ProjectStatusEnum.InProgress)
            {
                Status = ProjectStatusEnum.Finished;
                FinishedAt = DateTime.Now;
            }
        }

        // Método para atualizar os dados do projeto
        public void Update(string title, string description, decimal totalCost)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("O título do projeto não pode ser nulo ou vazio.", nameof(title));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("A descrição do projeto não pode ser nula ou vazia.", nameof(description));
            }

            if (totalCost < 0)
            {
                throw new ArgumentException("O custo total do projeto não pode ser negativo.", nameof(totalCost));
            }

            // Atualização das propriedades com os novos valores fornecidos
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }
    }
}
