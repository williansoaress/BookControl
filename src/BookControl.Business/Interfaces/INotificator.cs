using BookControl.Business.Notifications;
using System.Collections.Generic;

namespace BookControl.Business.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
