using DevFreela.Application.Commands.CreateUser; // Importa o namespace necessário para acessar o CreateUserCommand.
using FluentValidation; // Importa o namespace necessário para utilizar a FluentValidation.
using System.Text.RegularExpressions; // Importa o namespace necessário para utilizar expressões regulares.

namespace DevFreela.Application.Validators



{
    // Esta classe é responsável por validar o CreateUserCommand.
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {




        // No construtor da classe, são definidas as regras de validação para cada propriedade do CreateUserCommand.
        public CreateUserCommandValidator()
        {
            // Regra de validação para o campo Email, garantindo que seja um endereço de e-mail válido.
            RuleFor(user => user.Email)
                .EmailAddress() // Verifica se o valor do campo é um endereço de e-mail válido.
                .WithMessage("E-mail não válido!"); 

            // Regra de validação para o campo Password, usando uma função personalizada (ValidPassword) para verificar se a senha atende aos critérios especificados.
            RuleFor(user => user.passwordHash)
                .Must(ValidPassword) // Valida a senha utilizando a função ValidPassword.
                .WithMessage("Sua senha deve conter pelo menos 8 caracteres, um número, um caractere especial, uma letra maiúscula e uma minúscula"); 

            // Regra de validação para o campo FullName, garantindo que não esteja vazio ou nulo.
            RuleFor(user => user.FullName)
                .NotEmpty() // Garante que o campo não esteja vazio.
                .NotNull() // Garante que o campo não seja nulo.
                .WithMessage("Seu nome é obrigatório!"); 
        }







        // Método para validar a senha com base em uma expressão regular.
        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=].*$)");
            // A expressão regular verifica se a senha tem pelo menos 8 caracteres, um número, uma letra minúscula, uma letra maiúscula e um caractere especial.

            return regex.IsMatch(password); // Retorna true se a senha corresponder à expressão regular, caso contrário, retorna false.
        }
    }
}
