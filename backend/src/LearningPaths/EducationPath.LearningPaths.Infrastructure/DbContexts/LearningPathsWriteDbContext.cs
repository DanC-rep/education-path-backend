using EducationPath.LearningPaths.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationPath.LearningPaths.Infrastructure.DbContexts;

public class LearningPathsWriteDbContext : DbContext
{
    private readonly string _connectionString;

    public LearningPathsWriteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Roadmap> Roadmaps => Set<Roadmap>();
    
    public DbSet<Lesson> Lessons => Set<Lesson>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LearningPathsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("learning_paths");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}