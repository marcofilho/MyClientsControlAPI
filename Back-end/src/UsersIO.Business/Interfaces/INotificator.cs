using System.Collections.Generic;
using UsersIO.Business.Notifications;

namespace UsersIO.Business.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notificacao);
    }
}