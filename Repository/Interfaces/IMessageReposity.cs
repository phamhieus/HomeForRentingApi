using Data.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
  public interface INotificationRepository
  {
    Notification GetNotification(Guid id, bool trackChanges);

    IEnumerable<Notification> GetAllNotifications(bool trackChanges);

    IEnumerable<Notification> GetNotificationsOfUser(string userId, bool trackChanges);

    void CreateNotification(Notification notification);

    void DeleteNotification(Notification notification);

    void UpdateNotification(Notification notification);
  }
}
