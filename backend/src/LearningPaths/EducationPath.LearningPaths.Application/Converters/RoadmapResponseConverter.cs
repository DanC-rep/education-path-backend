using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Application.Converters;

public static class RoadmapResponseConverter
{
    public static Result<RoadmapAiResponse, Error> ConvertRoadmapResponse(string aiResponse)
    {
        if (string.IsNullOrWhiteSpace(aiResponse))
            return GeneralErrors.ValueIsInvalid();
        
        var lines = aiResponse
            .Replace("\r\n", "\n")
            .Replace('\r', '\n')
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in lines)
        {
            var colonIndex = line.IndexOf(':');
            if (colonIndex <= 0) continue;

            var key = line[..colonIndex].Trim();
            var rest = line[(colonIndex + 1)..].Trim();
            
            var semicolonIndex = rest.LastIndexOf(';');
            var value = semicolonIndex >= 0 ? rest[..semicolonIndex].Trim() : rest.Trim();

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
                continue;

            map[key] = value;
        }

        if (!map.TryGetValue("Title", out var title) || string.IsNullOrWhiteSpace(title))
            return GeneralErrors.ValueIsInvalid("Roadmap title");

        if (!map.TryGetValue("Description", out var description) || string.IsNullOrWhiteSpace(description))
            return GeneralErrors.ValueIsInvalid("Roadmap description");

        if (!map.TryGetValue("Count", out var countRaw) || string.IsNullOrWhiteSpace(countRaw))
            return GeneralErrors.ValueIsInvalid("Lessons count");

        if (!int.TryParse(countRaw, out var count) || count <= 0)
            return GeneralErrors.ValueIsInvalid();

        var response = new RoadmapAiResponse(title, description, count);

        return Result.Success<RoadmapAiResponse, Error>(response);
    }
}