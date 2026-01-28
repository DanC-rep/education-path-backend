using EducationPath.Core.Abstractions;
using EducationPath.LearningPaths.Contracts.Requests;

namespace EducationPath.LearningPaths.Application.UseCases.CreateRoadmap;

public record CreateRoadmapCommand(
    IEnumerable<Guid> SkillsIds,
    Guid UserId,
    int Level,
    string UserAdditionalData) : ICommand
{
    public static CreateRoadmapCommand Create(CreateRoadmapRequest request) 
        => new(request.SkillsIds, request.UserId, request.Level, request.UserAdditionalData);
}