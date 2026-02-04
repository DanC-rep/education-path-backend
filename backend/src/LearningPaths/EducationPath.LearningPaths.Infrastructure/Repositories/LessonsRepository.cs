using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Infrastructure.DbContexts;

namespace EducationPath.LearningPaths.Infrastructure.Repositories;

public class LessonsRepository : ILessonsRepository
{
    private readonly LearningPathsWriteDbContext _dbContext;

    public LessonsRepository(LearningPathsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Add(Lesson lesson, CancellationToken cancellationToken = default)
    {
        await _dbContext.Lessons.AddAsync(lesson, cancellationToken);
    }
}