using EducationPath.Core.Abstractions;

namespace EducationPath.LearningPaths.Application.UseCases.CompleteLesson;

public record CompleteLessonCommand(Guid LessonId) : ICommand;