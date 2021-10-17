using Data;
using Data.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implements
{
  public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
  {
    public NotificationRepository(DBContext repositoryContext) : base(repositoryContext)
    {
    }

    public Notification GetNotification(Guid id, bool trackChanges) =>
      FindByCondition(c => c.Id.Equals(id), trackChanges)
      .SingleOrDefault();

    public IEnumerable<Notification> GetAllNotifications(bool trackChanges) =>
     FindAll(trackChanges)
     .OrderBy(c => c.SentDate)
     .ToList();

    public void CreateNotification(Notification notification) => Create(notification);

    public void DeleteNotification(Notification notification) => Delete(notification);

    public void UpdateNotification(Notification notification) => Update(notification);

    public IEnumerable<Notification> GetNotificationsOfUser(string userId, bool trackChanges)=>
      FindByCondition(c => c.ToUser.Equals(userId), trackChanges)
      .OrderByDescending(n=>n.SentDate)
      .ToList();

  }
}
