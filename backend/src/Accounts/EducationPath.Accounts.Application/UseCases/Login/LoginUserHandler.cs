using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Contracts.Responses;
using EducationPath.Accounts.Domain;
using EducationPath.Accounts.Domain.Users;
using EducationPath.Core.Abstractions;
using EducationPath.SharedKernel.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationPath.Accounts.Application.UseCases.Login;

public class LoginUserHandler : ICommandHandler<LoginResponse, LoginUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginUserHandler> _logger;

    public LoginUserHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        ILogger<LoginUserHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }
    
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        LoginUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
            return GeneralErrors.NotFound(null, "user").ToErrors();
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);

        if (!passwordConfirmed)
            return AuthErrors.InvalidCredentials().ToErrors();

        var accessToken = await _tokenProvider.GenerateAccessToken(user, cancellationToken);
        var refreshToken = await _tokenProvider.GenerateRefreshToken(user, accessToken.Jti, cancellationToken);
        
        _logger.LogInformation("Successfully logged in");

        var userResponse = new UserResponse(
            user.Id,
            user.Email!,
            user.UserName!,
            user.Roles.Select(r => r.Name!.ToLower()));

        return new LoginResponse(accessToken.AccessToken, refreshToken, userResponse);
    }
}