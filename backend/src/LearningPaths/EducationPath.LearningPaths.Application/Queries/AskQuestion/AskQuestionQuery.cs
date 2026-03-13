using EducationPath.Core.Abstractions;

namespace EducationPath.LearningPaths.Application.Queries.AskQuestion;

public record AskQuestionQuery(Guid LessonId, string Question) : IQuery;
