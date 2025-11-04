using EducationPath.Skills.Application.Interfaces;
using EducationPath.Skills.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationPath.Skills.Infrastructure.DbContexts;

public class SkillsReadDbContext : DbContext, IReadDbContext
{
    private readonly string _connectionString;

    public SkillsReadDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IQueryable<SkillDto> Skills => Set<SkillDto>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("skills");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SkillsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}