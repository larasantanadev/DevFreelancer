using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    // Classe que representa o comando para criar um projeto
    public class CreateProjectCommand : IRequest<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int IdClient { get; set; }

        public int IdFreelancer { get; set; }

        public decimal TotalCost { get; set; }
    }
}

// Este comando encapsula as informações necessárias para cadastrar um novo projeto.
// ESSAS INFORMAÇÕES ESTAVAM NO INPUTMDODEL