using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;                                         // Importa o MediatR para lidar com padrão mediator
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;                        // Importa o namespace para criar controladores API

namespace DevFreela.API.Controllers
{


    // Controlador da API para manipular operações relacionadas a projetos
    [ApiController] // Indica que o controlador responde a requisições da API
    [Route("api/projects")] // Define o prefixo da rota para as ações deste controlador

    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator; // Instância do Mediator para lidar com requisições e respostas
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator; // Injeta o Mediator via construtor
        }






        // Endpoint para obter todos os projetos
        [HttpGet]
        [Authorize(Roles = "client, freelancer")] // autoriza o cliente e freelancer acessar este método
        public async Task<IActionResult> Get( string query)
        {
           
                // Cria uma instância da consulta para obter todos os projetos, passando uma possível string de consulta
                var getAllProjectsQuery = new GetAllProjectsQuery(query);

                // Envia a consulta ao mediador para processamento assíncrono
                var projects = await _mediator.Send(getAllProjectsQuery);

                // Retorna uma resposta HTTP 200 OK com os projetos obtidos
                return Ok(projects);
            
           
        }








        // Endpoint para criar um novo projeto
        [HttpPost]
        [Authorize(Roles = "client")]  // Apenas o cliente pode acessar este método

        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {

            // Envia o comando para criar o projeto ao Mediator e aguarda a resposta
            var id = await _mediator.Send(command);

            // Retorna uma resposta HTTP 201 Created com o ID do projeto criado
            // Também inclui o local do recurso criado para futura referência
            return CreatedAtAction(nameof(GetById), new { id }, command);

        }









        // Endpoint para obter um projeto por ID
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")] // autoriza o cliente e freelancer acessar este método

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                // Cria uma consulta para obter o projeto pelo ID
                var query = new GetProjectByIdQuery(id);

                // Envia a consulta ao mediador para processamento
                var project = await _mediator.Send(query);

                // Verifica se o projeto foi encontrado
                if (project == null)
                {
                    // Retorna uma resposta HTTP 404 Not Found se o projeto não for encontrado
                    return NotFound();
                }

                // Retorna uma resposta HTTP 200 OK com o projeto
                return Ok(project);
            }
            catch (Exception ex)
            {
                // Se ocorrer uma exceção, retorna uma resposta HTTP 500 Internal Server Error com detalhes da exceção
                return StatusCode(500, $"Ocorreu um erro ao obter o projeto: {ex.Message}");
            }
        }










        // Endpoint para atualizar um projeto existente
        [HttpPut("{id}")]
        [Authorize(Roles = "client")] // Apenas o cliente pode acessar este método

        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {

            await _mediator.Send(command);
                 
            return NoContent();                          
            // Retorna uma resposta HTTP 204 No Content
        }






        // Endpoint para excluir um projeto
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")] // Apenas o cliente pode acessar este método

        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);
            await _mediator.Send(command);                
            // Exclui o projeto com o ID fornecido

            return NoContent();                          
            // Retorna uma resposta HTTP 204 No Content
        }



        // Endpoint para adicionar um comentário a um projeto
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]  // autoriza o cliente e freelancer acessar este método

        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);   
            // Adiciona um comentário ao projeto com o ID fornecido

            return NoContent();                          
            // Retorna uma resposta HTTP 204 No Content
        }





        // Endpoint para iniciar um projeto
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]  // Apenas o cliente pode acessar este método

        public async Task<IActionResult> Start(int id, StartProjectCommand command)
        {

            await _mediator.Send(command);
           
            return NoContent();                          
            // Retorna uma resposta HTTP 204 No Content
        }






        // Endpoint para finalizar um projeto
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]  // Apenas o cliente pode acessar este método

        public async Task <IActionResult> Finish(int id, FinishProjectCommand command)
        {

            await _mediator.Send(command);

            return NoContent();                          
            // Retorna uma resposta HTTP 204 No Content
        }
    }
}
