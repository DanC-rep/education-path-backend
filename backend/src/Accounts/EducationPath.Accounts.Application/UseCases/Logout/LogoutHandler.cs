using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Core.Abstractions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Application.UseCases.Logout;

public class LogoutHandler : ICommandHandler<LogoutCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;

    public LogoutHandler(IRefreshSessionManager refreshSessionManager)
    {
        _refreshSessionManager = refreshSessionManager;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        var refreshSessionResult = await _refreshSessionManager.GetByUserId(command.UserId, cancellationToken);

        if (refreshSessionResult.IsFailure)
            return refreshSessionResult.Error.ToErrors();
        
        _refreshSessionManager.Delete(refreshSessionResult.Value);

        return Result.Success<ErrorList>();
    }
}