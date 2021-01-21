using FluentValidation;

namespace UsersIO.Business.Models.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Cpf.Length).Equal(11)
                .WithMessage("O campo CPF precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

            RuleFor(f => f.Rg.Length).Equal(7)
                .WithMessage("O campo RG precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");


        }
    }
}