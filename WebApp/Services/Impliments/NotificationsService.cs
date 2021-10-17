using AspImp.Services.Interfaces;

using Lib.AspNetCore.ServerSentEvents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspImp.Services.Impliments
{
  public class NotificationsService : INotificationsService
  {
    #region Fields
    private IServerSentEventsService _notificationsServerSentEventsService;
    #endregion

    #region Constructor
    public NotificationsService(
      IServerSentEventsService notificationsServerSentEventsService
      ) => _notificationsServerSentEventsService = notificationsServerSentEventsService;
    #endregion

    #region Methods
    public Task SendNotificationAsync(string notification, bool alert)
    {
      return _notificationsServerSentEventsService.SendEventAsync(new ServerSentEvent
      {
        Type = alert ? "alert" : null,
        Data = new List<string>(notification.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None))
      });
    }
    #endregion
  }
}
