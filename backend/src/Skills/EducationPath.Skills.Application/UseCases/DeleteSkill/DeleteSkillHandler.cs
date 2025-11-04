using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.SharedKernel.Errors;
using EducationPath.Skills.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationPath.Skills.Application.UseCases.DeleteSkill;

public class DeleteSkillHandler : ICommandHandler<DeleteSkillCommand>
{
    private readonly ISkillsRepository _repository;
    private readonly ILogger<DeleteSkillHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSkillHandler(
        ISkillsRepository repository,
        ILogger<DeleteSkillHandler> logger,
        [FromKeyedServices(Modules.Skills)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(DeleteSkillCommand command, CancellationToken cancellationToken = default)
    {
        var skill = await _repository.GetById(command.SkillId, cancellationToken);

        if (skill.IsFailure)
            return skill.Error.ToErrors();
        
        _repository.Delete(skill.Value, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.Log(LogLevel.Information, "Deleted skill with id: {id}", command.SkillId);

        return UnitResult.Success<ErrorList>();
    }
}