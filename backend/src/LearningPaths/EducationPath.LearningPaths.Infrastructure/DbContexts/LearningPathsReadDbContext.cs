using Microsoft.EntityFrameworkCore;
using EducationPath.LearningPaths.Application.Interfaces;
using EducationPath.LearningPaths.Contracts.Dtos;
using Microsoft.Extensions.Logging;

namespace EducationPath.LearningPaths.Infrastructure.DbContexts;

public class LearningPathsReadDbContext : DbContext, IReadDbContext
{
    private readonly string _connectionString;

    public LearningPathsReadDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IQueryable<RoadmapDto> Roadmaps => Set<RoadmapDto>();
    
    public IQueryable<LessonDto> Lessons => Set<LessonDto>();
    
    public IQueryable<LessonDependencyDto> LessonDependencies => Set<LessonDependencyDto>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("learning_paths");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LearningPathsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}