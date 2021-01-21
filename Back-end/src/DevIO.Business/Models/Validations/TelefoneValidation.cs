using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class TelefoneValidation : AbstractValidator<Telefone>
    {
        public TelefoneValidation()
        {
            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8, 14).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}