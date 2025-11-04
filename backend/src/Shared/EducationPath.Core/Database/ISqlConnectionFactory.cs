using System.Data;

namespace EducationPath.Core.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}