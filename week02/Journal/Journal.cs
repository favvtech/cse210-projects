using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class Journal
{
    private readonly List<Entry> _entries = new List<Entry>();

    private const string DefaultSeparator = "~|~";

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public IEnumerable<Entry> GetEntries()
    {
        return _entries;
    }

    public void Clear()
    {
        _entries.Clear();
    }

    public void SaveToFile(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        if (ext == ".csv")
        {
            SaveCsv(filePath);
            return;
        }
        if (ext == ".json")
        {
            SaveJson(filePath);
            return;
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(DefaultSeparator);
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.Serialize(DefaultSeparator));
            }
        }
    }

    public void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found.", filePath);
        }

        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        if (ext == ".csv")
        {
            LoadCsv(filePath);
            return;
        }
        if (ext == ".json")
        {
            LoadJson(filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        if (lines.Length == 0)
        {
            _entries.Clear();
            return;
        }

        string separator = lines[0];
        _entries.Clear();
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            _entries.Add(Entry.Deserialize(line, separator));
        }
    }

    private void SaveCsv(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("Date,Prompt,Response");
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(string.Join(",", CsvEscape(entry.DateText), CsvEscape(entry.Prompt), CsvEscape(entry.Response)));
            }
        }
    }

    private void LoadCsv(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        _entries.Clear();
        int startIndex = 0;
        if (lines.Length > 0 && lines[0].StartsWith("Date,"))
        {
            startIndex = 1; // skip header
        }
        for (int i = startIndex; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            List<string> fields = ParseCsvLine(line);
            if (fields.Count >= 3)
            {
                _entries.Add(new Entry(fields[0], fields[1], fields[2]));
            }
        }
    }

    private void SaveJson(string filePath)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(filePath, json, Encoding.UTF8);
    }

    private void LoadJson(string filePath)
    {
        string json = File.ReadAllText(filePath, Encoding.UTF8);
        List<Entry> loaded = JsonSerializer.Deserialize<List<Entry>>(json) ?? new List<Entry>();
        _entries.Clear();
        foreach (Entry entry in loaded)
        {
            _entries.Add(entry);
        }
    }

    private static string CsvEscape(string value)
    {
        if (value == null)
        {
            return "\"\""; // empty quotes
        }
        string escaped = value.Replace("\"", "\"\"");
        return "\"" + escaped + "\"";
    }

    private static List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();
        StringBuilder current = new StringBuilder();
        bool inQuotes = false;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (inQuotes)
            {
                if (c == '"')
                {
                    bool isEscapedQuote = (i + 1 < line.Length && line[i + 1] == '"');
                    if (isEscapedQuote)
                    {
                        current.Append('"');
                        i++; // skip second quote
                    }
                    else
                    {
                        inQuotes = false; // end quote
                    }
                }
                else
                {
                    current.Append(c);
                }
            }
            else
            {
                if (c == ',')
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else if (c == '"')
                {
                    inQuotes = true;
                }
                else
                {
                    current.Append(c);
                }
            }
        }
        fields.Add(current.ToString());
        return fields;
    }
}


