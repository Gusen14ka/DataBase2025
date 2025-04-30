using System.Reflection;

namespace STO.Core.Models;

public class OrderedService
{
	public int Id { get; private set; }
    public int OrderId { get; private set; }
    public Order? Order { get; private set; } // Навигация
    public int ServiceId { get; private set; }
    public Service? Service { get; private set; } // Навигация
    public int Quantity { get; private set; }

    private OrderedService(
        int id,
        int orderId,
        int serviceId,
        int quantity,
        Order? order = null,
        Service? service = null)
    {
        Id = id;
        OrderId = orderId;
        ServiceId = serviceId;
        Quantity = quantity;
        Order = order;
        Service = service;
    }

    public static OrderedService FactoryOrderService(
        int id,
        int orderId,
        int serviceId,
        int quantity,
        Order? order = null,
        Service? service = null)
    {
        if (id <= 0)
            throw new ArgumentException("Некорректный Id.");
        if (orderId <= 0)
            throw new ArgumentException("Некорректный OrderId.");
        if (serviceId <= 0)
            throw new ArgumentException("Некорректный ServiceId.");
        if (quantity <= 0)
            throw new ArgumentException("Некорректный Quantity.");
        if (order != null)
        {
            if (order.Id != orderId)
                throw new ArgumentException("Некорректный Order.");
        }
        if (service != null)
        {
            if (service.Id != serviceId)
                throw new ArgumentException("Некорректный Service.");
        }

        return new OrderedService(
            id: id,
            orderId: orderId,
            serviceId: serviceId,
            quantity: quantity,
            order: order,
            service: service
        );
    }
}
