using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Domain.Entities;
using EducationPath.LearningPaths.Infrastructure.DbContexts;

namespace EducationPath.LearningPaths.Infrastructure.Repositories;

public class RoadmapsRepository : IRoadmapsRepository
{
    private readonly LearningPathsWriteDbContext _writeDbContext;

    public RoadmapsRepository(LearningPathsWriteDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }
    
    public async Task Add(Roadmap roadmap, CancellationToken cancellationToken = default)
    {
        await _writeDbContext.Roadmaps.AddAsync(roadmap, cancellationToken);
    }
}