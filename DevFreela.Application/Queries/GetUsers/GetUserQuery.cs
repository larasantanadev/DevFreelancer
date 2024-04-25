using DevFreela.Application.ViewModels;
using MediatR;


namespace DevFreela.Application.Queries.GetUsers
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
