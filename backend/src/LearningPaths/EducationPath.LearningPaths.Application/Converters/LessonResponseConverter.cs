using System.Text;
using CSharpFunctionalExtensions;
using EducationPath.LearningPaths.Contracts.Responses;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.LearningPaths.Application.Converters;

public static class LessonResponseConverter
{
    public static Result<LessonAiResponse, Error> ConvertLessonResponse(string aiResponse)
    {
        if (string.IsNullOrWhiteSpace(aiResponse))
            return GeneralErrors.ValueIsInvalid();

        var text = aiResponse.Replace("\r\n", "\n").Replace('\r', '\n');
        var lines = text.Split('\n');

        var title = (string?)null;
        var contentSb = new StringBuilder();
        var links = new List<string>();
        var next = new List<int>();
        var prev = new List<int>();
        int? type = null;

        var section = Section.None;

        for (int i = 0; i < lines.Length; i++)
        {
            var raw = lines[i];
            var line = raw.TrimEnd();

            if (TryReadHeader(line, "Title:", out var titleValue))
            {
                section = Section.Title;
                title = titleValue.Trim();
                continue;
            }

            if (TryReadHeader(line, "Content:", out var contentFirstLine))
            {
                section = Section.Content;
                contentSb.Clear();
                if (!string.IsNullOrWhiteSpace(contentFirstLine))
                    contentSb.AppendLine(contentFirstLine);
                continue;
            }

            if (TryReadHeader(line, "Resources:", out var linksValue))
            {
                section = Section.Links;
                links.Clear();
                links.AddRange(ParseStringList(linksValue));
                continue;
            }

            if (TryReadHeader(line, "Next lessons:", out var nextValue))
            {
                section = Section.Next;
                next.Clear();
                next.AddRange(ParseIntList(nextValue));
                continue;
            }

            if (TryReadHeader(line, "Prev lessons:", out var prevValue))
            {
                section = Section.Prev;
                prev.Clear();
                prev.AddRange(ParseIntList(prevValue));
                continue;
            }

            if (TryReadHeader(line, "Lesson Type:", out var typeValue))
            {
                section = Section.Type;
                int.TryParse(typeValue, out var value);
                type = value;
                continue;
            }

            if (section == Section.Content)
                contentSb.AppendLine(raw);
        }
        
        var content = contentSb.ToString().Trim();
        
        if (string.IsNullOrWhiteSpace(title))
            return GeneralErrors.ValueIsInvalid("Lesson title");

        if (string.IsNullOrWhiteSpace(content))
            return GeneralErrors.ValueIsInvalid("Lesson content");

        if (next.Any(n => n < 1))
            return GeneralErrors.ValueIsInvalid("Next lessons");

        if (prev.Any(n => n < 1))
            return GeneralErrors.ValueIsInvalid("Prev lessons");
        
        if (type is null)
            return GeneralErrors.ValueIsInvalid("Lesson type");

        return Result.Success<LessonAiResponse, Error>(new LessonAiResponse(
            title!,
            content,
            next,
            prev,
            links,
            type.Value));
    }


    private enum Section { None, Title, Content, Links, Next, Prev, Type }

    private static bool TryReadHeader(string line, string header, out string value)
    {
        if (line.StartsWith(header, StringComparison.OrdinalIgnoreCase))
        {
            value = line.Substring(header.Length).Trim().TrimEnd(';').Trim();
            return true;
        }

        value = string.Empty;
        return false;
    }

    private static IEnumerable<string> ParseStringList(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];

        return value.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static IEnumerable<int> ParseIntList(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];
        
        if (value.StartsWith("[") && value.EndsWith("]"))
            value = value[1..^1].Trim();

        if (string.IsNullOrWhiteSpace(value))
            return [];
        
        return value
            .Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s => int.TryParse(s, out var n) ? (int?)n : null)
            .Where(n => n.HasValue)
            .Select(n => n!.Value);
    }

}