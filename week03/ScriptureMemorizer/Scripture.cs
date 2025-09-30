using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;
    private readonly Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = SplitIntoWords(text);
    }

    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden || string.IsNullOrWhiteSpace(w.GetDisplayText()));
    }

    public void HideRandomWords(int count)
    {
        // Core requirement: can select any word at random, even if already hidden
        for (int i = 0; i < count && _words.Count > 0; i++)
        {
            int index = _random.Next(_words.Count);
            _words[index].Hide();
        }
    }

    public void HideRandomVisibleWords(int count)
    {
        // Stretch: only hide words that are not already hidden and contain letters
        var candidates = _words
            .Select((w, i) => new { w, i })
            .Where(x => !x.w.IsHidden && HasAnyLetter(x.w))
            .Select(x => x.i)
            .ToList();

        for (int i = 0; i < count && candidates.Count > 0; i++)
        {
            int pick = _random.Next(candidates.Count);
            int index = candidates[pick];
            _words[index].Hide();
            candidates.RemoveAt(pick);
        }
    }

    public string GetDisplayText()
    {
        var builder = new StringBuilder();
        builder.Append(_reference.ToString());
        builder.Append(" - ");
        for (int i = 0; i < _words.Count; i++)
        {
            builder.Append(_words[i].GetDisplayText());
            if (i < _words.Count - 1)
            {
                builder.Append(' ');
            }
        }
        return builder.ToString();
    }

    private static bool HasAnyLetter(Word word)
    {
        string text = word.GetDisplayText();
        return text.Any(char.IsLetter);
    }

    private static List<Word> SplitIntoWords(string text)
    {
        var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var list = new List<Word>(parts.Length);
        foreach (var part in parts)
        {
            list.Add(new Word(part));
        }
        return list;
    }
}



