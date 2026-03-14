using EducationPath.Tests.Application.Interfaces;
using EducationPath.Tests.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EducationPath.Tests.Infrastructure.DbContexts;

public class TestsReadDbContext : DbContext, IReadDbContext
{
    private readonly string _connectionString;

    public TestsReadDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IQueryable<TestDto> Tests => Set<TestDto>();

    public IQueryable<TestQuestionDto> TestQuestions => Set<TestQuestionDto>();
    
    public IQueryable<QuestionAnswerDto> QuestionAnswers => Set<QuestionAnswerDto>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tests");

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(TestsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
}