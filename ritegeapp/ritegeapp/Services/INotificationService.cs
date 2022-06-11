using RitegeDomain.DTO;
using RitegeDomain.Model;

namespace ritegeapp.Services
{
    public interface INotificationService
    {
        void CreateAlertNotification(EventDTO parkingEvent);
    }
}