using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.SharedKernel.Errors;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.LearningPaths.Application.UseCases.CompleteLesson;

public class CompleteLessonHandler : ICommandHandler<CompleteLessonCommand>
{
    private readonly ILessonsRepository _lessonsRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CompleteLessonHandler(
        ILessonsRepository lessonsRepository,
        [FromKeyedServices(Modules.LearingPaths)] IUnitOfWork unitOfWork)
    {
        _lessonsRepository = lessonsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(CompleteLessonCommand command, CancellationToken cancellationToken = default)
    {
        var lessonResult = await _lessonsRepository.GetById(command.LessonId, cancellationToken);

        if (lessonResult.IsFailure)
            return lessonResult.Error.ToErrors();

        lessonResult.Value.Complete();

        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}