using EducationPath.Accounts.Domain.Users;

namespace EducationPath.Accounts.Domain.RefreshTokens;

public class RefreshSession
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public User User { get; init; }
    
    public Guid RefreshToken { get; init; }
    
    public Guid Jti { get; set; }
    
    public DateTime ExpirationToken { get; set; }
    
    public DateTime CreationDate { get; set; }
}