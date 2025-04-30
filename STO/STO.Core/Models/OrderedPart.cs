namespace STO.Core.Models;

public class OrderedPart
{
	public int Id { get; private set; }
	public int OrderId { get; private set; }
	public Order? Order { get; private set; } // Навигация
	public int PartId { get; private set; }
	public Part? Part { get; private set; } // Навигация
	public int Quantity { get; private set; }

	private OrderedPart(
		int id,
		int orderId,
		int partId,
		int quantity,
		Order? order = null,
		Part? part = null)
	{
		Id = id;
		OrderId = orderId;
		PartId = partId;
		Quantity = quantity;
		Order = order;
		Part = part;
	}

	public static OrderedPart FactoryOrderPart(
		int id,
		int orderId,
		int partId,
		int quantity,
		Order? order = null,
		Part? part = null)
	{
		if (id <= 0)
			throw new ArgumentException("Некорректный Id.");
		if (orderId <= 0)
			throw new ArgumentException("Некорректный OrderId.");
		if (partId <= 0)
			throw new ArgumentException("Некорректный PartId.");
		if (quantity <= 0)
			throw new ArgumentException("Некорректный Quantity.");
		if (order != null)
		{
			if (order.Id != orderId)
				throw new ArgumentException("Некорректный Order.");
		}
		if (part != null)
		{
			if (part.Id != partId)
				throw new ArgumentException("Некорректный Part.");
		}

		return new OrderedPart(
			id: id,
			orderId: orderId,
			partId: partId,
			quantity: quantity,
			order: order,
			part: part
		);

	}
}
