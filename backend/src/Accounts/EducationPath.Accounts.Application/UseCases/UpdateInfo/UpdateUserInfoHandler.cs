using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain.Users.ValueObjects;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.Core.Validation;
using EducationPath.SharedKernel.Errors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Accounts.Application.UseCases.UpdateInfo;

public class UpdateUserInfoHandler : ICommandHandler<UpdateUserInfoCommand>
{
    private readonly IValidator<UpdateUserInfoCommand> _validator;
    private readonly IAccountsManager _accountManager;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateUserInfoHandler(
        IValidator<UpdateUserInfoCommand> validator,
        IAccountsManager accountManager,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _validator = validator;
        _accountManager = accountManager;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToList();
        
        var userResult = await _accountManager.GetById(command.Id, cancellationToken);

        if (userResult.IsFailure)
            return userResult.Error.ToErrors();
        
        var fullName = FullName.Create(
            command.FullName.Name, 
            command.FullName.Surname, 
            command.FullName.Patronymic);
        
        userResult.Value.FullName = fullName.Value;

        await _unitOfWork.SaveChanges(cancellationToken);

        return Result.Success<ErrorList>();
    }
}