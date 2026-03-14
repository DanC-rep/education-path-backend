using System.Data.Common;
using EducationPath.Core.Database;
using EducationPath.Tests.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace EducationPath.Tests.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly TestsWriteDbContext _writeDbContext;

    public UnitOfWork(TestsWriteDbContext writeDbContext)
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