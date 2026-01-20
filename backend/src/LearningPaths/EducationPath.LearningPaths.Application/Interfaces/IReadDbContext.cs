using EducationPath.LearningPaths.Contracts.Dtos;

namespace EducationPath.LearningPaths.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<RoadmapDto> Roadmaps { get; }
    
    IQueryable<LessonDto> Lessons { get; }
}