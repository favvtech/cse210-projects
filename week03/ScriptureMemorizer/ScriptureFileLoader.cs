using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class ScriptureRecord
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndVerse { get; set; }
    public string Text { get; set; }
}

public static class ScriptureFileLoader
{
    public static List<Scripture> LoadFromJson(string fileName)
    {
        var results = new List<Scripture>();

        string? path = ResolvePath(fileName);
        if (path == null || !File.Exists(path))
        {
            return results;
        }

        try
        {
            string json = File.ReadAllText(path);
            var records = JsonSerializer.Deserialize<List<ScriptureRecord>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (records == null)
            {
                return results;
            }

            foreach (var rec in records)
            {
                if (rec == null || string.IsNullOrWhiteSpace(rec.Book) || string.IsNullOrWhiteSpace(rec.Text))
                {
                    continue;
                }

                Reference reference = rec.EndVerse.HasValue
                    ? new Reference(rec.Book, rec.Chapter, rec.StartVerse, rec.EndVerse.Value)
                    : new Reference(rec.Book, rec.Chapter, rec.StartVerse);

                results.Add(new Scripture(reference, rec.Text));
            }
        }
        catch
        {
            return new List<Scripture>();
        }

        return results;
    }

    private static string? ResolvePath(string fileName)
    {
        string cwd = Directory.GetCurrentDirectory();
        string candidate1 = Path.Combine(cwd, fileName);
        if (File.Exists(candidate1))
        {
            return candidate1;
        }

        string baseDir = AppContext.BaseDirectory;
        string candidate2 = Path.Combine(baseDir, fileName);
        if (File.Exists(candidate2))
        {
            return candidate2;
        }

        string? parent = Directory.GetParent(baseDir)?.Parent?.Parent?.FullName;
        if (parent != null)
        {
            string candidate3 = Path.Combine(parent, fileName);
            if (File.Exists(candidate3))
            {
                return candidate3;
            }
        }

        return null;
    }
}




