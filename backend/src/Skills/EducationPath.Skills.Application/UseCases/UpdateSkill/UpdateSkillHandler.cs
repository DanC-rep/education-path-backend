using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.Core.Validation;
using EducationPath.SharedKernel.Errors;
using EducationPath.SharedKernel.ValueObjects;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Application.UseCases.CreateSkill;
using EducationPath.Skills.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationPath.Skills.Application.UseCases.UpdateSkill;

public class UpdateSkillHandler : ICommandHandler<UpdateSkillCommand>
{
    private readonly ISkillsRepository _repository;
    private readonly ILogger<UpdateSkillHandler> _logger;
    private readonly IValidator<UpdateSkillCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSkillHandler(
        ISkillsRepository repository,
        ILogger<UpdateSkillHandler> logger,
        IValidator<UpdateSkillCommand> validator,
        [FromKeyedServices(Modules.Skills)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(UpdateSkillCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToList();

        var skill = await _repository.GetById(command.SkillId, cancellationToken);

        if (skill.IsFailure)
            return skill.Error.ToErrors();

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        
        skill.Value.UpdateInformation(name, description);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.Log(LogLevel.Information, "Updated Skill {SkillId}", command.SkillId);

        return UnitResult.Success<ErrorList>();
    }
}