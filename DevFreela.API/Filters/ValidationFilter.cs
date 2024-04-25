using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter

    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Verifica se o estado do modelo recebido pela ação é inválido
            if (!context.ModelState.IsValid)
            {
                // Se houver erros de validação no modelo, coleta as mensagens de erro
                var messages = context.ModelState
                    .SelectMany(msg => msg.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Define o resultado da ação como um BadRequestObjectResult,
                // que retorna um status HTTP 400 (Bad Request) junto com as mensagens de erro de validação
                context.Result = new BadRequestObjectResult(messages);
            }
        }

    }
}




// Com esse filtro a gente consegue validar todos os endpoints, todos os acessos de API's.
// Para não repetir códigos, e deixar os controllers mais limpos. 