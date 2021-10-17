using System.Threading.Tasks;

namespace AspImp.Services.Interfaces
{
  public interface INotificationsService
  {
    Task SendNotificationAsync(string notification, bool alert);
  }
}
