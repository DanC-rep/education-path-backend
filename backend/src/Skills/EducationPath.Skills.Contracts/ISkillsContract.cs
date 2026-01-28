using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Skills.Contracts;

public interface ISkillsContract
{
    Task<Result<IEnumerable<string>, Error>> GetSkills(IEnumerable<Guid> skillsIds, CancellationToken cancellationToken = default);
}