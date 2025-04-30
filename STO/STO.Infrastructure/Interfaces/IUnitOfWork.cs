namespace STO.Infrastructure.Interfaces;
public interface IUnitOfWork
{
    // Открывает транзакцию.
    Task BeginTransactionAsync();

    // Выполняет все накопленные изменения и коммитит транзакцию.
    Task CommitAsync();

    // Откатывает транзакцию.
    Task RollbackAsync();

    // Сохраняет накопленные изменения (эквивалент SaveChangesAsync).
    Task<int> SaveChangesAsync();
}