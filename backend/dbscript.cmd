docker-compose up -d

dotnet-ef database drop -f -c AccountsWriteDbContext -p .\src\Accounts\EducationPath.Accounts.Infrastructure\ -s .\src\EducationPath.Web\

dotnet-ef migrations remove -c AccountsWriteDbContext -p .\src\Accounts\EducationPath.Accounts.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef migrations remove -c LearningPathsWriteDbContext -p .\src\LearningPaths\EducationPath.LearningPaths.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef migrations remove -c SkillsWriteDbContext -p .\src\Skills\EducationPath.Skills.Infrastructure\ -s .\src\EducationPath.Web\

dotnet-ef migrations add Accounts_Initial -c AccountsWriteDbContext -p .\src\Accounts\EducationPath.Accounts.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef migrations add LearningPaths_Initial -c LearningPathsWriteDbContext -p .\src\LearningPaths\EducationPath.LearningPaths.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef migrations add Skills_Initial -c SkillsWriteDbContext -p .\src\Skills\EducationPath.Skills.Infrastructure\ -s .\src\EducationPath.Web\

dotnet-ef database update -c AccountsWriteDbContext -p .\src\Accounts\EducationPath.Accounts.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef database update -c LearningPathsWriteDbContext -p .\src\LearningPaths\EducationPath.LearningPaths.Infrastructure\ -s .\src\EducationPath.Web\
dotnet-ef database update -c SkillsWriteDbContext -p .\src\Skills\EducationPath.Skills.Infrastructure\ -s .\src\EducationPath.Web\

pause