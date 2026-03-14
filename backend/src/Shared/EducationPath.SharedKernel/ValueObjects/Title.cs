using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.SharedKernel.ValueObjects;

public class Title : ValueObject
{
    public const int MAX_TITLE_LENGTH = 100;
    
    public string Value { get; private set; }
    
    private Title() { }
    
    private Title(string value) => Value = value;

    public static Result<Title, Error> Create(string title)
    {
        if (string.IsNullOrEmpty(title))
            return GeneralErrors.ValueIsRequired("title");

        if (title.Length > MAX_TITLE_LENGTH)
            return GeneralErrors.Length("title", MAX_TITLE_LENGTH);

        return new Title(title);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}