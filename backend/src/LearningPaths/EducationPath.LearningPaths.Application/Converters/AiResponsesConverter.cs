using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Application.Converters;

public static class AiResponsesConverter
{
    public static Result<RoadmapAiResponse, Error> ConvertRoadmapResponse(string aiResponse)
    {
        return GeneralErrors.ValueIsInvalid();
    }

    public static Result<LessonAiResponse, Error> ConvertLessonResponse(string aiResponse)
    {
        return GeneralErrors.ValueIsInvalid();
    }
}