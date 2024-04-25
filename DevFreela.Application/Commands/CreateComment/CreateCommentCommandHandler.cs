using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities; // Importa as entidades do projeto DevFreela
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using MediatR; // Importa o MediatR para lidar com padrão mediator
using System.Threading; // Importa para lidar com threads
using System.Threading.Tasks; // Importa para lidar com tarefas assíncronas

namespace DevFreela.Application.Commands.CreateComment
{
    // Classe que manipula o comando de criação de comentário
    // Classe responsável por manipular o comando de atualizar um projeto



    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository; // Declaração do repositório de projetos

        // Construtor para inicializar o contexto do banco de dados
        public CreateCommentCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository; // Atribui o contexto do banco de dados à variável local
        }









        // Método para lidar com a criação de um novo comentário
        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser); 
            // Cria uma nova instância de ProjectComment com os dados fornecidos

            // Adiciona o comentário ao contexto do banco de dados
            await _projectRepository.AddCommentAsync(comment);

            return Unit.Value; // Retorna Unit.Value para indicar que o comando foi executado com sucesso
        }

    }
}
