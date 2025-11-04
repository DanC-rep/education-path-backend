using CSharpFunctionalExtensions;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.Core.Validation;
using EducationPath.SharedKernel.Errors;
using EducationPath.SharedKernel.ValueObjects.Ids;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Domain.Entities;
using EducationPath.Skills.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationPath.Skills.Application.UseCases.CreateSkill;

public class CreateSkillHandler : ICommandHandler<Guid, CreateSkillCommand>
{
    private readonly ISkillsRepository _repository;
    private readonly ILogger<CreateSkillHandler> _logger;
    private readonly IValidator<CreateSkillCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSkillHandler(
        ISkillsRepository repository,
        ILogger<CreateSkillHandler> logger,
        IValidator<CreateSkillCommand> validator,
        [FromKeyedServices(Modules.Skills)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateSkillCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToList();

        var name = Name.Create(command.Name).Value;
        var description = Description.Create(command.Description).Value;
        var skillId = SkillId.NewId();

        Skill? parentSkill = null;

        if (command.ParentId != null)
        {
            var parentSkillId = SkillId.Create(command.ParentId.Value);
            var parentSkillResult = await _repository.GetById(parentSkillId, cancellationToken);

            if (parentSkillResult.IsFailure)
                return parentSkillResult.Error.ToErrors();

            parentSkill = parentSkillResult.Value;
        }

        Skill? skill = null;

        skill = parentSkill != null 
            ? new Skill(skillId, name, description, command.ParentId, parentSkill) 
            : new Skill(skillId, name, description);

        await _repository.Add(skill, cancellationToken);
        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.Log(LogLevel.Information, "Created new skill with id: {id}", (Guid)skill.Id);

        return (Guid)skill.Id;
    }
}