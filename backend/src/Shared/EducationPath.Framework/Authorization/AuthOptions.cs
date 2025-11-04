namespace EducationPath.Framework.Authorization;

public class AuthOptions
{
    public const string AUTH = nameof(AUTH);
    
    public string Audience { get; init; }
    
    public string Issuer { get; init; }
    
    public string Key { get; init; }
    
    public string ExpiredMinutesTime { get; init; }
}