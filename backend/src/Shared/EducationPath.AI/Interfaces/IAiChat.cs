using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.AI.Interfaces;

public interface IAiChat
{
    Task<Result<string, Error>> SendPrompt(
        string prompt,
        CancellationToken cancellationToken = default);
}