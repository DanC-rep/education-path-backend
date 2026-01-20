using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Domain.ValueObjects;

public class Link : ValueObject
{
    // TODO: добавить нормальную валидацию
    public string Value { get; private set; }
    
    private Link() { }

    public Link(string value) => Value = value;

    public static Result<Link, Error> Create(string link)
    {
        return new Link(link);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}