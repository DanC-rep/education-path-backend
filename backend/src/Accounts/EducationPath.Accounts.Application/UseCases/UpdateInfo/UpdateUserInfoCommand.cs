using EducationPath.Core.Abstractions;
using EducationPath.Core.Dtos;

namespace EducationPath.Accounts.Application.UseCases.UpdateInfo;

public record UpdateUserInfoCommand(Guid Id, FullNameDto FullName) : ICommand;