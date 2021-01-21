using FluentValidation;

namespace UsersIO.Business.Models.Validations
{
    public class SocialMidiaValidation : AbstractValidator<SocialMidia>
    {
        public SocialMidiaValidation()
        {
            RuleFor(c => c.Value)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(25, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
