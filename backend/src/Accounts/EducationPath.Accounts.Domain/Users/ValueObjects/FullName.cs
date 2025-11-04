using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Domain.Users.ValueObjects;

public class FullName : ValueObject
{
    private FullName(string name, string surname, string? patronymic)
    {
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
    }
    
    private FullName()
    {
    }
    
    public string Name { get; } = null!;

    public string Surname { get; } = null!;

    public string? Patronymic { get; } = null!;

    public static Result<FullName, Error> Create(string name, string surname, string? patronymic)
    {
        if (string.IsNullOrWhiteSpace(name))
            return GeneralErrors.ValueIsRequired("name");

        if (string.IsNullOrWhiteSpace(surname))
            return GeneralErrors.ValueIsRequired("surname");

        return new FullName(name, surname, patronymic);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
        yield return Patronymic ?? string.Empty;
    }
}