using CSharpFunctionalExtensions;
using EducationPath.Accounts.Application.Interfaces;
using EducationPath.Accounts.Contracts.Dtos;
using EducationPath.Accounts.Contracts.Responses;
using EducationPath.Core.Abstractions;
using EducationPath.SharedKernel.Errors;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Accounts.Application.Queries.GetUserById;

public class GetUserByIdHandler : IQueryHandlerWithResult<GetUserByIdResponse, GetUserByIdQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetUserByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<GetUserByIdResponse, ErrorList>> Handle(
        GetUserByIdQuery query, 
        CancellationToken cancellationToken = default)
    {
        var user = await GetUserById(query.UserId, cancellationToken);

        if (user is null)
            return GeneralErrors.NotFound(query.UserId, "user").ToErrors();

        return GetUserByIdResponse.Create(user);
    }

    private async Task<UserDto?> GetUserById(Guid userId, CancellationToken cancellationToken = default) =>
        await _readDbContext.Users
            .Include(u => u.AdminAccount)
            .Include(u => u.StudentAccount)
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
}