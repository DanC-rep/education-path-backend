using System.Text;

namespace EducationPath.AI.Prompts;

public static class CreateLessonPrompt
{
    public static string GetPrompt(int lessonNumber)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Давай сгенерируем урок под номером {lessonNumber}. Используй следующий формат для твоего ответа. " +
                      $"Пожалуйста следуй строго \"key: value\" формату в ответе. Не добавляй ссылки на ресурсы в части контента." +
                      $"В контенте опиши небольшую теорию, можешь добавить несколько примеров. " +
                      $"А в ресурсах укажи ссылки на источники, где стоит продолжать дальнейшее обучение." +
                      $"Также необходимо, чтобы ты добавил по крайней мере один урок типа Optional или Recommended. Но большинство" +
                      $"уроков должно быть типа Required. Учти что у карты обучения должен быть последний урок, у которого нет следующих.");

        sb.AppendLine("Title: [название урока]")
          .AppendLine("Content: [Содержимое урока в формате markdown (максимальная длина: 2000 symbols)]")
          .AppendLine("Resources: [список ссылок, разделяемых символом ;]")
          .AppendLine("Lesson Type: [Required = 0 | Optional = 1 | Recommended = 2]")
          .AppendLine("Next lessons: [список следующих уроков, разделяемых символом ;]")
          .AppendLine("Prev lessons: [список предыдущих уроков, разделяемых символом ;]");

        sb.AppendLine("Пример ответа по части ресурсов, предыдущих и следующих уроков. В списках next и prev lessons указываются" +
                      "номера уроков, которые должны следовать сразу за / после текущего. Следуй строго заданному формату ответа " +
                      "и не добавляй лишних символов между ключом с значением:")
            .AppendLine("Lesson Type: 1")
            .AppendLine("Resources: https://learn.microsoft.com/en-us/ef/core/;https://learn.microsoft.com/en-us/ef/core/")
            .AppendLine("Next lessons: [2, 4]");

        return sb.ToString();
    }    
}