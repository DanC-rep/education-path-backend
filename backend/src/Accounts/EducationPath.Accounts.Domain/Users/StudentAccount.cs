namespace EducationPath.Accounts.Domain.Users;

public class StudentAccount
{
    private StudentAccount()
    {
        
    }

    public StudentAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
    }
    
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public Guid UserId { get; set; }
}