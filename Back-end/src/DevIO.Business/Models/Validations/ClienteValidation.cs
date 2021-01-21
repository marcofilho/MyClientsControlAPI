using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Cpf)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(11, 11)
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
            
            RuleFor(f => f.Rg)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .Length(6, 7)
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.DataNascimento)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
             .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}