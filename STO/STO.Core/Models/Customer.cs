namespace STO.Core.Models;

public class Customer
{
	public int Id { get; private set; }

    private bool _isDeleted;
    public bool IsDeleted => _isDeleted;
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    private Customer(
        int id,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        bool isDeleted = false)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        _isDeleted = isDeleted;
    }

    public static Customer FactoryCustomer(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        bool isDeleted = false,
        int? id = null)
    {
        if (id != null)
        {
            if (id <= 0)
                throw new ArgumentException("Некорректный Id.");
        }
        if (firstName.Length > 100)
            throw new ArgumentException("Некорректное Имя.");
        if (lastName.Length > 100)
            throw new ArgumentException("Некорректная Фамилия.");
        if (phoneNumber.Length > 100)
            throw new ArgumentException("Некорректный Номер телефона.");
        if (email.Length > 100)
            throw new ArgumentException("Некорректный Email.");

        return new Customer(
            id: id ?? 0,
            firstName: firstName,
            lastName: lastName,
            email: email,
            phoneNumber: phoneNumber,
            isDeleted: isDeleted
        );
    }
    public void MarkAsDelete()
    {
        _isDeleted = true;
    }

}
