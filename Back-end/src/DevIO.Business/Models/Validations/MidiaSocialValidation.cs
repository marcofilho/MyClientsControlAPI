using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class MidiaSocialValidation : AbstractValidator<MidiaSocial>
    {
        public MidiaSocialValidation()
        {
            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}