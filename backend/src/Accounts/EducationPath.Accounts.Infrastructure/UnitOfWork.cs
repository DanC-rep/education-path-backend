using System.Data.Common;
using EducationPath.Accounts.Infrastructure.DbContexts;
using EducationPath.Core.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace EducationPath.Accounts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AccountsWriteDbContext _writeDbContext;

    public UnitOfWork(AccountsWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    
    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _writeDbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _writeDbContext.SaveChangesAsync(cancellationToken);
    }
}