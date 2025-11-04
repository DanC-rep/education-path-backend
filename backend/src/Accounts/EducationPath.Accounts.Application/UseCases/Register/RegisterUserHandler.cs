using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Domain;
using EducationPath.Accounts.Domain.Roles;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Accounts.Domain.Users.ValueObjects;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.Core.Validation;
using EducationPath.SharedKernel.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EducationPath.Accounts.Application.UseCases.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IAccountsManager _accountsManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IAccountsManager accountsManager,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork,
        ILogger<RegisterUserHandler> logger,
        IValidator<RegisterUserCommand> validator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _accountsManager = accountsManager;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _validator = validator;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToList();

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(r => r.Name == Constants.STUDENT, cancellationToken);

            if (role is null)
                return GeneralErrors.NotFound(null, "role").ToErrors();

            var fullName = FullName.Create(
                command.FullName.Name,
                command.FullName.Surname,
                command.FullName.Patronymic).Value;

            var userResult = User.CreateStudent(
                command.UserName,
                command.Email,
                fullName,
                role);

            if (userResult.IsFailure)
                return userResult.Error.ToErrors();

            var result = await _userManager.CreateAsync(userResult.Value, command.Password);

            if (!result.Succeeded)
                return GeneralErrors.Failure("can not register user").ToErrors();

            var studentAccount = new StudentAccount(userResult.Value);

            await _accountsManager.AddStudentAccount(studentAccount, cancellationToken);

            await _unitOfWork.SaveChanges(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("user was created with username {userName}", command.UserName);

            return Result.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError("User registration wa failed");

            await transaction.RollbackAsync(cancellationToken);

            return GeneralErrors.Failure("can not register user").ToErrors();
        }
    }
}