using System.Text;

namespace EducationPath.AI.Prompts;

public static class CreateLessonPrompt
{
    public static string GetPrompt(int lessonNumber)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Let's generate a lesson number {lessonNumber}. Use the following format for your answer:");

        sb.AppendLine("Title: [lesson title]")
          .AppendLine("Content: [lesson content in .md format]")
          .AppendLine("Links: [additional links to resources through ;]")
          .AppendLine("Next lessons: [next lessons numbers through ;]")
          .AppendLine("Prev lessons: [previous lessons numbers through ;]");

        return sb.ToString();
    }    
}