using DevFreela.Application.Commands.CreateProject; // Importa o namespace necessário para acessar o CreateProjectCommand.
using FluentValidation; // Importa o namespace necessário para utilizar a FluentValidation.
namespace DevFreela.Application.Validators
{
    // Esta classe é responsável por validar o CreateProjectCommand.
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {




        // No construtor da classe, são definidas as regras de validação para cada propriedade do CreateProjectCommand.
        public CreateProjectCommandValidator()
        {
            // Regra de validação para a propriedade Description, limitando o tamanho máximo a 255 caracteres.
            RuleFor(project => project.Description)
                .MaximumLength(255) // Define o tamanho máximo permitido para a descrição.
                .WithMessage("O tamanho máximo da descrição é de 255 caracteres."); // Mensagem de erro a ser exibida se a validação falhar.






            // Regra de validação para a propriedade Title, limitando o tamanho máximo a 30 caracteres.
            RuleFor(project => project.Title)
                .MaximumLength(30) // Define o tamanho máximo permitido para o título.
                .WithMessage("O tamanho máximo do título é de 30 caracteres."); // Mensagem de erro a ser exibida se a validação falhar.
        }
    }
}
