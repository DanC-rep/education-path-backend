namespace EducationPath.LearningPaths.Contracts.Responses;

public record UserRoadmapsResponse(IEnumerable<RoadmapGeneralInfoResposne> Roadmaps);

public record RoadmapGeneralInfoResposne(Guid Id, string Title, string Descriptions, string Level);