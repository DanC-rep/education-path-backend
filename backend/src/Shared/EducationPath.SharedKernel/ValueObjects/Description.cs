using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.SharedKernel.ValueObjects;

public class Description : ValueObject
{
    public const int MAX_DESCRIPTON_LENGTH = 500;
    
    public string Value { get; private set; }

    private Description()
    {
    }
    
    private Description(string value) => Value = value;

    public static Result<Description, Error> Create(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return GeneralErrors.ValueIsRequired("description");
        
        if (description.Length > MAX_DESCRIPTON_LENGTH)
            return GeneralErrors.Length("description", MAX_DESCRIPTON_LENGTH);

        return new Description(description);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}