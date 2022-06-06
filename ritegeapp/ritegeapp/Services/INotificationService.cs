using RitegeDomain.Model;

namespace ritegeapp.Services
{
    public interface INotificationService
    {
        void CreateAlertNotification(ParkingEvent parkingEvent);
    }
}