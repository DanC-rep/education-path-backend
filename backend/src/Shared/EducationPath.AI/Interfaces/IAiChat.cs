using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;
using OllamaSharp;

namespace EducationPath.AI.Interfaces;

public interface IAiChat
{
    Task<Result<string, Error>> SendPrompt(
        Chat client,
        string prompt,
        CancellationToken cancellationToken = default);

    Chat InitClient();
}