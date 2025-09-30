using System;

public class Entry
{
    public string DateText { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string dateText, string prompt, string response)
    {
        DateText = dateText;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"Date: {DateText} - Prompt: {Prompt}\n{Response}";
    }

    public string Serialize(string separator)
    {
        return string.Join(separator, new [] { DateText, Prompt, Response });
    }

    public static Entry Deserialize(string line, string separator)
    {
        string[] parts = line.Split(new[] { separator }, StringSplitOptions.None);
        if (parts.Length < 3)
        {
            throw new FormatException("Invalid entry format.");
        }
        return new Entry(parts[0], parts[1], parts[2]);
    }
}


