using EducationPath.Core.Abstractions;

namespace EducationPath.LearningPaths.Application.Queries.GetRoadmapsByUser;

public record GetUserRoadmapsQuery(Guid UserId) : IQuery;