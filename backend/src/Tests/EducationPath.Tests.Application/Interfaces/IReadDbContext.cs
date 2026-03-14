using EducationPath.Tests.Contracts.Dtos;

namespace EducationPath.Tests.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<TestDto> Tests { get; }
    
    IQueryable<TestQuestionDto> TestQuestions { get; }
    
    IQueryable<QuestionAnswerDto> QuestionAnswers { get; }
}