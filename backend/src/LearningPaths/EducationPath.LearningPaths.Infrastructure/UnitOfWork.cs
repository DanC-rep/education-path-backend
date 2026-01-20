using System.Data.Common;
using EducationPath.Core.Database;
using EducationPath.LearningPaths.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace EducationPath.LearningPaths.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly LearningPathsWriteDbContext _writeDbContext;

    public UnitOfWork(LearningPathsWriteDbContext writeDbContext)
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