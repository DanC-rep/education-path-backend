using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Infrastructure.DbContexts;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Result<Lesson, Error>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        if (lesson is null)
            return GeneralErrors.NotFound(id, "lesson");

        return lesson;
    }
}