using FluentValidation;
using FluentValidation.Results;
using UsersIO.Business.Interfaces;
using UsersIO.Business.Models;
using UsersIO.Business.Notifications;

namespace UsersIO.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificador;

        protected BaseService(INotificator notificador)
        {
            _notificador = notificador;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }

        protected bool ExecuteNotify<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}