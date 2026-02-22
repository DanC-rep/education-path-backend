using CSharpFunctionalExtensions;
using EducationPath.Accounts.Contracts;
using EducationPath.Core.Abstractions;
using EducationPath.LearningPaths.Application.Converters;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.LearningPaths.Domain.Enums;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Application.Queries.GetRoadmapsByUser;

public class GetRoadmapsByUserHandler : IQueryHandlerWithResult<UserRoadmapsResponse, GetUserRoadmapsQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IAccountsContract _accountsContract;

    public GetRoadmapsByUserHandler(
        IReadDbContext readDbContext,
        IAccountsContract accountsContract)
    {
        _readDbContext = readDbContext;
        _accountsContract = accountsContract;
    }

    public async Task<Result<UserRoadmapsResponse, ErrorList>> Handle(
        GetUserRoadmapsQuery query, 
        CancellationToken cancellationToken = default)
    {
        var userExists = await _accountsContract.CheckUserExists(query.UserId, cancellationToken);

        if (userExists.IsFailure)
            return userExists.Error.ToErrors();

        var roadmaps = _readDbContext.Roadmaps.Where(r => r.UserId == query.UserId).ToList();

        var generalInfos = roadmaps.Select(r => new RoadmapGeneralInfoResposne(
            r.Id,
            r.Title,
            r.Description,
            RoadmapLevelConverter.Convert((RoadmapLevel)r.Level)));

        return new UserRoadmapsResponse(generalInfos);
    }
}
