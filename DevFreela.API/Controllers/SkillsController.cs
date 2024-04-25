using DevFreela.Application.Queries.GetAllSkills; // Importa a consulta para obter todas as habilidades
using MediatR; // Importa o MediatR para lidar com padrão mediator
using Microsoft.AspNetCore.Mvc; // Importa o namespace para criar controladores da Web API
using System.Threading.Tasks; // Importa o namespace para lidar com tarefas assíncronas

namespace DevFreela.API.Controllers
{
    // Controlador da Web API para manipular operações relacionadas a habilidades
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediator; // Instância do Mediator para enviar consultas

        // Construtor que inicializa o controlador com o Mediator
        public SkillsController(IMediator mediator)
        {
            _mediator = mediator; // Atribui o Mediator à variável local
        }

        // Endpoint para obter todas as habilidades
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllSkillsQuery(); // Cria uma nova instância da consulta para obter todas as habilidades

            var skills = await _mediator.Send(query); // Envia a consulta para o Mediator para processamento

            return Ok(skills); // Retorna uma resposta HTTP 200 OK com as habilidades obtidas
        }
    }
}
