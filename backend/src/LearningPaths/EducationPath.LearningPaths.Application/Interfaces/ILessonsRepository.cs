using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Application.Interfaces;

public interface ILessonsRepository
{
    Task Add(Lesson lesson, CancellationToken cancellationToken = default);
    
    Task<Result<Lesson, Error>> GetById(Guid id, CancellationToken cancellationToken = default);
}