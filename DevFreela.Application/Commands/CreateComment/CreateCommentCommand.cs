using MediatR;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>   // UNIT = não tem retorno
    {
        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }


    }
}


// Este comando encapsula as informações necessárias para cadastrar um novo comentário.
// ESSAS INFORMAÇÕES ESTAVAM NO INPUTMDODEL