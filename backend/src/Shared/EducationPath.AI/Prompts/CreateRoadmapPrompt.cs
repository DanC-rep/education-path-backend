using System.Text;

namespace EducationPath.AI.Prompts;

public static class CreateRoadmapPrompt
{
    public static string GetPrompt(
        IEnumerable<string> skills,
        string level,
        string userAdditionalInfo) // skillDto
    {
        var sb = new StringBuilder();

        sb.AppendLine("You'll need to create a learning roadmap. Use the following list of skills you need to master as a basis: " +
                      $"{string.Join(",", skills)}");

        sb.AppendLine($"Current level of user knowledge: {level}");
        
        sb.AppendLine("The user also added an additional description. If you think this isn't relevant to the roadmap," +
                      $" simply reply with \"incorrect description.\" Here's the user's description: {userAdditionalInfo}");

        sb.AppendLine("To get started, simply provide a title, description, and the number of lessons included " +
                      "in the roadmap. Keep in mind that one lesson may be followed by several others. " +
                      "Use this format for your response:");

        sb.AppendLine("Title: [roadmap title];")
          .AppendLine("Description: [roadmap description];")
          .AppendLine("Count: [lessons count];");

        return sb.ToString();
    }
}