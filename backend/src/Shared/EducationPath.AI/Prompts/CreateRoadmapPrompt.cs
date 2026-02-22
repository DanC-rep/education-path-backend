using System.Text;

namespace EducationPath.AI.Prompts;

public static class CreateRoadmapPrompt
{
    public static string GetPrompt(
        IEnumerable<string> skills,
        string level,
        string userAdditionalInfo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Тебе надо составить план обучения. Используй список навыков, которые пользователю надо освоить, в качестве основы: " +
                      $"{string.Join(",", skills)}");

        sb.AppendLine($"Текущий уровень знаний пользователя: {level}");
        
        sb.AppendLine("Пользователь также добавил доп. описание. Если ты считаешь, что это не имеет отношения к плану обучения," +
                      $" в ответе просто верни \"incorrect description.\" Вот описание от пользователя: {userAdditionalInfo}");

        sb.AppendLine("Для начала просто укажите название, описание и количество включенных уроков " +
                      "в плане обучения. Имейте в виду, что за одним уроком может последовать сразу несколько других. " +
                      "Используй следующий формат для своего ответа:");

        sb.AppendLine("Title: [название плана обучения];")
          .AppendLine("Description: [описание плана обучения];")
          .AppendLine("Count: [количество уроков (максимум 8)];");

        return sb.ToString();
    }
}