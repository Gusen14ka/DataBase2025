using System.Reflection;

namespace STO.Core.Models;

public class Timetable
{
    public int Id { get; private set; }
    public int ServiceId { get; private set; }
    public Service? Service { get; private set; }
    public int CarId { get; private set; }
    public Car? Car { get; private set; }
    public DateTime NextVisit { get; private set; }

    private Timetable(
        int id,
        int serviceId,
        int carId,
        DateTime nextVisit,
        Service? service = null,
        Car? car = null)
    {
        Id = id;
        ServiceId = serviceId;
        CarId = carId;
        NextVisit = nextVisit;
        Service = service;
        Car = car;
    }

    public static Timetable FactoryTimetable(
        int serviceId,
        int carId,
        DateTime nextVisit,
        Service? service = null,
        Car? car = null,
        int? id = null)
    {
        if (id != null)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный Id.");
        }
        if (serviceId <= 0)
            throw new ArgumentException("Некорректный ServiceId.");
        if (carId <= 0)
            throw new ArgumentException("Некорректный CarId.");
        if (nextVisit < DateTime.UtcNow)
            throw new ArgumentException("Некорректный NextVisit.");
        if (service != null)
        {
            if (service.Id != serviceId)
                throw new ArgumentException("Некорректный Service.");
        }
        if (car != null)
        {
            if (car.Id != carId)
                throw new ArgumentException("Некорректный Car.");
        }

        return new Timetable(
            id: id ?? 0,
            serviceId: serviceId,
            carId: carId,
            nextVisit: nextVisit,
            service: service,
            car: car
        );
    }
}
