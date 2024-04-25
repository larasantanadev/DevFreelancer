using DevFreela.Application.Commands.CreateUser; // Importa o namespace para o comando CreateUserCommand
using DevFreela.Application.Commands.LoginUser; // Importa o namespace para o comando LoginUserCommand
using DevFreela.Application.Queries.GetUsers; // Importa o namespace para a consulta GetUserQuery
using MediatR; // Importa o namespace para o IMediator
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; // Importa o namespace para o ControllerBase
using System.Threading.Tasks; // Importa o namespace para tarefas assíncronas

namespace DevFreela.API.Controllers
{




    // Controlador da API para manipular operações relacionadas a usuários
    [Route("api/users")] // Define o prefixo de rota para todas as rotas neste controlador


    [Authorize] // Especifica que todas as ações deste controlador requerem autenticação para serem acessadas
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator; // Instância do Mediator para lidar com requisições e respostas

        // Construtor que injeta uma instância de IMediator
        public UsersController(IMediator mediator)
        {
            _mediator = mediator; // Injeta o Mediator via construtor
        }








        // Método de endpoint para obter um usuário pelo seu ID
        // Rota: api/users/{id}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Cria uma consulta GetUserQuery com o ID fornecido
            var query = new GetUserQuery(id);

            // Envia a consulta para o mediador para processamento e aguarda o resultado
            var user = await _mediator.Send(query);

            // Se o usuário não for encontrado, retorna uma resposta NotFound (404)
            if (user == null)
            {
                return NotFound();
            }

            // Retorna uma resposta Ok (200) contendo os detalhes do usuário encontrado
            return Ok(user);
        }







        // Método de endpoint para criar um novo usuário
        // Rota: api/users

        [HttpPost] // Define que este método responde a requisições HTTP POST
        [AllowAnonymous] // Permite acesso anônimo a este método, ou seja, não requer autenticação

        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            // Envia o comando CreateUserCommand para o mediador para processamento e aguarda o resultado
            var id = await _mediator.Send(command);

            // Retorna uma resposta de sucesso (201 - Created) contendo o recurso recém-criado (usuário) e sua localização
            // A localização é definida como a ação GetById do controlador, que pode ser usada para recuperar os detalhes do usuário recém-criado
            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }






        // Método de endpoint para lidar com a solicitação de login de usuário
        // Rota: api/users/login
        [HttpPut("login")]
        [AllowAnonymous] // Permite acesso anônimo a este método, ou seja, não requer autenticação

        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            // Envia o comando LoginUserCommand para o mediador para processamento
            var loginUserViewModel = await _mediator.Send(command);

            // Se o viewModel retornado for nulo, significa que as credenciais de login estão incorretas
            // Retorna uma resposta BadRequest para indicar um erro de solicitação
            if (loginUserViewModel == null)
            {
                return BadRequest();
            }

            // Retorna uma resposta Ok (200) contendo o viewModel do usuário logado
            return Ok(loginUserViewModel);
        }
    }
}
