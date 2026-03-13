using System.Text;

namespace EducationPath.AI.Prompts;

public static class AskQuestionPrompt
{
    public static string GetPrompt(string lessonTitle, string lessonContent, string question)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Тебе необходимо дать краткий ответ на вопрос по уроку: {lessonTitle}")
          .AppendLine($"Соедржимое урока: {lessonContent}")
          .AppendLine($"Вопрос, на который надо ответить: {question}")
          .Append("Ели вопрос не относится к теме урока, просто верни: Некорректный вопрос.");

        return sb.ToString();
    }
}
