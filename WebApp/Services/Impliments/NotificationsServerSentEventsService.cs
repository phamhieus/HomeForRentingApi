using AspImp.Services.Interfaces;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.Extensions.Options;

namespace AspImp.Services.Impliments
{
  internal class NotificationsServerSentEventsService : 
    ServerSentEventsService, INotificationsServerSentEventsService
  {
    public NotificationsServerSentEventsService(IOptions<ServerSentEventsServiceOptions<NotificationsServerSentEventsService>> options)
        : base(options.ToBaseServerSentEventsServiceOptions())
    { }
  }
}
