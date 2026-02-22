using EducationPath.Core.Abstractions;

namespace EducationPath.LearningPaths.Application.Queries.GetRoadmap;

public record GetRoadmapQuery(Guid Id ) : IQuery;