namespace EducationPath.SharedKernel.Errors;

public static class GeneralErrors
{
    public static Error ValueIsInvalid(string? name = null)
    {
        var label = name ?? "значение";
        return Error.Validation("value.is.invalid", $"{label} недействительно");
    }

    public static Error NotFound(Guid? id = null, string? name = null)
    {
        var forId = id == null ? string.Empty : $" по Id '{id}'";
        return Error.NotFound("record.not.found", $"{name ?? "запись"} не найдена{forId}");
    }

    public static Error ValueIsRequired(string? name = null)
    {
        var label = name == null ? string.Empty : " " + name + " ";
        return Error.Validation("length.is.invalid", $"Поле{label}обязательно");
    }

    public static Error AlreadyExist()
        => Error.Validation("record.already.exist", "Запись уже существует");

    public static Error Failure(string? message = null)
        => Error.Failure("server.failure", message ?? "Серверная ошибка");
    
    public static Error Length(string name, int requiredLength)
        => Error.Failure("length.is.invalid", $"Длина {name} должна не превышать {requiredLength}");
}