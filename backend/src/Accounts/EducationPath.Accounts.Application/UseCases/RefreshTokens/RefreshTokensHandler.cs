using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Contracts.Responses;
using EducationPath.Accounts.Domain;
using EducationPath.Core.Abstractions;
using EducationPath.Core.Database;
using EducationPath.Core.Enums;
using EducationPath.SharedKernel.Errors;
using Microsoft.Extensions.DependencyInjection;

namespace EducationPath.Accounts.Application.UseCases.RefreshTokens;

public class RefreshTokensHandler : ICommandHandler<LoginResponse, RefreshTokensCommand>
{
    private readonly IRefreshSessionManager _refreshSessionManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokensHandler(
        IRefreshSessionManager refreshSessionManager,
        ITokenProvider tokenProvider,
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _refreshSessionManager = refreshSessionManager;
        _tokenProvider = tokenProvider;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        RefreshTokensCommand command, 
        CancellationToken cancellationToken = default)
    {
        var oldRefreshSessionResult = await _refreshSessionManager
            .GetByRefreshToken(command.RefreshToken, cancellationToken);

        if (oldRefreshSessionResult.IsFailure)
            return oldRefreshSessionResult.Error.ToErrors();

        if (oldRefreshSessionResult.Value.ExpirationToken < DateTime.UtcNow)
            return AuthErrors.ExpiredToken().ToErrors();

        _refreshSessionManager.Delete(oldRefreshSessionResult.Value);
        await _unitOfWork.SaveChanges(cancellationToken);

        var accessToken = await _tokenProvider.GenerateAccessToken(
            oldRefreshSessionResult.Value.User, 
            cancellationToken);
        
        var refreshToken = await _tokenProvider
            .GenerateRefreshToken(oldRefreshSessionResult.Value.User, accessToken.Jti, cancellationToken);

        var user = new UserResponse(
            oldRefreshSessionResult.Value.User.Id,
            oldRefreshSessionResult.Value.User.Email!,
            oldRefreshSessionResult.Value.User.UserName!,
            oldRefreshSessionResult.Value.User.Roles.Select(r => r.Name!.ToLower()));

        return new LoginResponse(accessToken.AccessToken, refreshToken, user);
    }
}