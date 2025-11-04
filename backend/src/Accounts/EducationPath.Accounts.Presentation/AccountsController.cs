using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Queries.GetUserById;
using EducationPath.Accounts.Application.UseCases.Login;
using EducationPath.Accounts.Application.UseCases.RefreshTokens;
using EducationPath.Accounts.Application.UseCases.Register;
using EducationPath.Accounts.Contracts.Requests;
using EducationPath.Accounts.Contracts.Responses;
using EducationPath.Framework;
using EducationPath.Framework.Authorization;
using EducationPath.Framework.EndpointResults;
using EducationPath.SharedKernel;
using EducationPath.SharedKernel.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EducationPath.Accounts.Presentation;

public class AccountsController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<EndpointResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = RegisterUserCommand.Create(request);
        
        return await handler.Handle(command, cancellationToken);
    }
    
    [HttpPost("login")]
    [ProducesResponseType<Envelope<LoginResponse>>(200)]
    public async Task<EndpointResult<LoginResponse>> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        
        return await handler.Handle(command, cancellationToken);
    }
    
    [HttpPost("refresh")]
    [ProducesResponseType<Envelope<LoginResponse>>(200)]
    public async Task<EndpointResult<LoginResponse>> RefreshTokens(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new RefreshTokensCommand(request.RefreshToken);
        
        return await handler.Handle(command, cancellationToken);
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType<Envelope<GetUserByIdResponse>>(200)]
    public async Task<EndpointResult<GetUserByIdResponse>> GetUserById(
        [FromRoute] Guid userId,
        [FromServices] GetUserByIdHandler handler)
    {
        var query = new GetUserByIdQuery(userId);
        
        return await handler.Handle(query);
    }

    [Permission("student.permission")]
    [HttpGet("authorize-test")]
    public EndpointResult TestAuthorize()
    {
        return UnitResult.Success<Error>();
    }
}