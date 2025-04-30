using System.Data;

namespace STO.Core.Models;

public class Order
{
	public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public Customer? Customer { get; private set; } // Навигация
    public int CarId { get; private set; }
    public Car? Car { get; private set; } // Навигация
    public DateTime CreatedTime { get; private set; }
    public DateTime? FinishedTime { get; private set; }
    public int Speedometer { get; private set; }
    public bool IsFinished { get; private set; }

    private Order(
        int id,
        int customerId,
        int carId,
        DateTime createdTime,
        int speedometer,
        bool isFinished,
        Customer? customer = null,
        Car? car = null,
        DateTime? finishedTime = null)
    {
        Id = id;
        CustomerId = customerId;
        CarId = carId;
        CreatedTime = createdTime;
        Speedometer = speedometer;
        IsFinished = isFinished;
        Customer = customer;
        Car = car;
        FinishedTime = finishedTime;
    }

    public static Order FactoryOrder(
        int customerId,
        int carId,
        DateTime? createdTime,
        int speedometer,
        bool isFinished = false,
        Customer? customer = null,
        Car? car = null,
        DateTime? finishedTime = null,
        int? id = null)
    {
        if (id != null)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный Id.");
        }
        if (customerId <= 0)
            throw new ArgumentException("Некорректный CustomerId.");
        if (carId <= 0)
            throw new ArgumentException("Некорректный CarId.");
        if (speedometer <= 0)
            throw new ArgumentException("Некорректный Speedometer.");
        if (customer != null)
        {
            if (customer.Id != customerId)
                throw new ArgumentException("Некорректный Customer.");
        }

        if (car != null)
        {
            if (car.Id != carId)
                throw new ArgumentException("Некорректный Car.");
        }

        if (isFinished && finishedTime == null)
            finishedTime = DateTime.UtcNow;

        DateTime effectiveCreatedTime;
        if (!createdTime.HasValue || createdTime.Value == DateTime.MinValue)
        {
            effectiveCreatedTime = DateTime.UtcNow;
        }
        else
        {
            effectiveCreatedTime = createdTime.Value;
        }

        return new Order(
            id: id ?? 0,
            customerId: customerId,
            carId: carId,
            createdTime: effectiveCreatedTime,
            speedometer: speedometer,
            isFinished: isFinished,
            customer: customer,
            car: car,
            finishedTime: finishedTime
        );
    }

    public void MarkAsFinished()
    {
        IsFinished = true;
    }
}
