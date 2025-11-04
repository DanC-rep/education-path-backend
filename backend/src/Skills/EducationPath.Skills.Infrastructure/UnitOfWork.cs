using System.Data.Common;
using EducationPath.Core.Database;
using EducationPath.Skills.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace EducationPath.Skills.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly SkillsWriteDbContext _writeDbContext;

    public UnitOfWork(SkillsWriteDbContext writeDbContext)
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