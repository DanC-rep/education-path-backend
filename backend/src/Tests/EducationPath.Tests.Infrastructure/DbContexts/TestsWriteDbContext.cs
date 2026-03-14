using EducationPath.Tests.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationPath.Tests.Infrastructure.DbContexts;

public class TestsWriteDbContext : DbContext
{
    private readonly string _connectionString;

    public TestsWriteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public DbSet<Test> Tests => Set<Test>();
    
    public DbSet<TestQuestion> TestQuestions => Set<TestQuestion>();
    
    public DbSet<QuestionAnswer> QuestionAnswers => Set<QuestionAnswer>();
    
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
            typeof(TestsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("tests");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => builder.AddConsole());
}