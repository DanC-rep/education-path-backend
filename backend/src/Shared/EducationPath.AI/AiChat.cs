using CSharpFunctionalExtensions;
using EducationPath.AI.Interfaces;
using EducationPath.SharedKernel.Errors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OllamaSharp;

namespace EducationPath.AI;

public class AiChat : IAiChat
{
    private readonly string _connectionString;
    private readonly ILogger<AiChat> _logger;

    public AiChat(
        IConfiguration configuration,
        ILogger<AiChat> logger)
    {
        _connectionString = configuration.GetConnectionString("AI")!;
        _logger = logger;
    }
    
    public async Task<Result<string, Error>> SendPrompt(
        Chat client,
        string prompt, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            List<string> response = [];

            await foreach (var answerToken in client.SendAsync(prompt, cancellationToken))
                response.Add(answerToken);

            return string.Join("", response);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while sending prompt {message}", ex.Message);
            
            return Error.Failure("sending.prompt", "Error while sending prompt");
        }
    }

    public Chat InitClient()
    {
        var client = new OllamaApiClient(_connectionString)
        {
            SelectedModel = "gemma3"
        };

        return new Chat(client);
    }
}