using System.Text;

namespace EducationPath.AI.Prompts;

public static class CreateLessonPrompt
{
    public static string GetPrompt(int lessonNumber)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Let's generate lesson number {lessonNumber}. Use the following format for your answer. " +
                      $"Please use the strict \"key: value\" format when answering. Don`t push resources to the content part." +
                      $"In the content, describe a brief theory on the topic, perhaps including examples. " +
                      $"And in the resources, provide links for further study of the topic.");

        sb.AppendLine("Title: [lesson title]")
          .AppendLine("Content: [lesson content in .md format (max length: 1500 symbols)]")
          .AppendLine("Resources: [additional links to resources through ;]")
          .AppendLine("Lesson Type: [Required | Optional | Recommended]")
          .AppendLine("Next lessons: [next lessons numbers through ;]")
          .AppendLine("Prev lessons: [previous lessons numbers through ;]");

        return sb.ToString();
    }    
}