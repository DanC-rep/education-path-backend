using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Skills.Domain.ValueObjects;

public class Name : ValueObject
{
    public const int MAX_NAME_LENGTH = 400;
    
    public string Value { get; private set; }

    private Name()
    {
    }
    
    private Name(string value) => Value = value;

    public static Result<Name, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return GeneralErrors.ValueIsRequired("description");
        
        if (description.Length > MAX_NAME_LENGTH)
            return GeneralErrors.Length("description", MAX_NAME_LENGTH);

        return new Name(description);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}