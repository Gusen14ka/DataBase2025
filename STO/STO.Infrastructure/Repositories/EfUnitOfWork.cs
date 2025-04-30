using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using STO.Infrastructure.Data;
using STO.Infrastructure.Interfaces;

namespace STO.Infrastructure.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _tx;

    public EfUnitOfWork(AppDbContext db) => _db = db;

    public async Task BeginTransactionAsync()
    {
        _tx = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_tx == null) throw new InvalidOperationException("Transaction not started");
        await _db.SaveChangesAsync();
        await _tx.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_tx == null) return;
        await _tx.RollbackAsync();
    }

    public Task<int> SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}