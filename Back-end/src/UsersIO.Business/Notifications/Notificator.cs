using System.Collections.Generic;
using System.Linq;
using UsersIO.Business.Interfaces;

namespace UsersIO.Business.Notifications
{
    public class Notificator : INotificator
    {
        private List<Notification> _notification;

        public Notificator()
        {
            _notification = new List<Notification>();
        }

        public void Handle(Notification notificacao)
        {
            _notification.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notification;
        }

        public bool HasNotification()
        {
            return _notification.Any();
        }
    }
}