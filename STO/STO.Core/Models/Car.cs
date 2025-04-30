using System.Diagnostics;

namespace STO.Core.Models;

public class Car
{
	public int Id { get; private set; }
    public int ModelId { get; private set; } //Внешний ключ
    public Model? Model { get; private set; } // Навигация
    public int Year { get; private set; }
    public string Vin { get; private set; }

    private bool _isDeleted;
    public bool IsDeleted => _isDeleted;
    public int CustomerId { get; private set; }  // Внешний ключ (ссылка на клиента)
    public Customer? Customer { get; private set; }  // Навигационное свойство (связь с клиентом)
    public DateTime StartService { get; private set; }
    public DateTime? EndService { get; private set; }

    // Приватный конструктор
    private Car(
        int id,
        int modelId,
        int year,
        string vin,
        int customerId,
        DateTime startService,
        DateTime? endService,
        bool isDeleted,
        Model? model = null,
        Customer? customer = null)
    {
        Id = id;
        ModelId = modelId;
        Year = year;
        Vin = vin;
        CustomerId = customerId;
        StartService = startService;
        EndService = endService;
        Model = model;
        Customer = customer;
        _isDeleted = isDeleted;
    }

    public static Car FactoryCar(
        int modelId,
        int year,
        string vin,
        int customerId,
        DateTime? startService,
        DateTime? endService = null,
        Model? model = null,
        Customer? customer = null,
        bool isDeleted = false,
        int? id = null)
    {
        // Пример валидации:
        if (year < 1900 || year > DateTime.UtcNow.Year)
            throw new ArgumentException("Некорректный год автомобиля.");
        if (vin.Length <= 0)
            throw new ArgumentException("Некорректный VIN.");
        if (modelId <= 0)
            throw new ArgumentException("Некорректный ModelId.");
        if (customerId <= 0)
            throw new ArgumentException("Некорректный CustomerId.");
        if (id != null)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный Id.");
        }
        if (model != null)
        {
            if (model.Id != modelId)
                throw new ArgumentException("Некорректный Model.");
        }
        if (customer != null)
        {
            if (customer.Id != customerId)
                throw new ArgumentException("Некорректный Customer.");
        }
        var effectiveStartService = startService ?? DateTime.UtcNow;





        return new Car(
            id: id ?? 0,
            modelId: modelId,
            year: year,
            vin: vin,
            customerId: customerId,
            startService: effectiveStartService,
            endService: endService,
            isDeleted: isDeleted,
            model: model,
            customer: customer
        );
    }
    public void MarkAsDeleted()
    {
        EndService = DateTime.UtcNow;
        _isDeleted = true;
    }
}
