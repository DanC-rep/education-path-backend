using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Infrastructure.DbContexts;

namespace EducationPath.LearningPaths.Infrastructure.Repositories;

public class LessonsDependenciesRepository : ILessonsDependenciesRepository
{
    private readonly LearningPathsWriteDbContext _dbContext;

    public LessonsDependenciesRepository(LearningPathsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddRange(IEnumerable<LessonDependency> dependencies, CancellationToken cancellationToken)
    {
        await _dbContext.AddRangeAsync(dependencies, cancellationToken);
    }
}