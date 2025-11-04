using EducationPath.Core.Abstractions;
using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Contracts.Responses;
using Microsoft.EntityFrameworkCore;

namespace EducationPath.Skills.Application.Queries.GetAllSkills;

public class GetAllSkillsHandler : IQueryHandler<GetAllSkillsResponse>
{
    private readonly IReadDbContext _readDbContext;

    public GetAllSkillsHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }


    public GetAllSkillsResponse Handle()
    {
        var skills = _readDbContext.Skills
            .Include(n => n.Children)
            .Where(s => s.ParentId == null);
        
        return new GetAllSkillsResponse(skills.ToList());
    }
}