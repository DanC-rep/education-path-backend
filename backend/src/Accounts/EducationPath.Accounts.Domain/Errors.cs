using EducationPath.SharedKernel.Errors;

namespace EducationPath.Accounts.Domain;

public static class AuthErrors
{
    public static Error InvalidCredentials()
    {
        return Error.Validation("credentials.is.invalid", "Неверные учетные данные");
    }

    public static Error InvalidRole()
    {
        return Error.Failure("role.is.invalid", "Неверная роль пользователя");
    }

    public static Error InvalidToken()
    {
        return Error.Validation("token.is.invalid", "Ваш токен недействителен");
    }

    public static Error ExpiredToken()
    {
        return Error.Validation("token.is.expired", "Ваш токен истек");
    }
}