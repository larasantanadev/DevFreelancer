using MediatR;


namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Unit>
    {
        public DeleteProjectCommand()
        {
        }

        public DeleteProjectCommand(int id)
        {
            Id = Id;
        }

        public int Id { get; private set; }
    }
}
