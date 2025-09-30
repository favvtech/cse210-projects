using System;
using System.Text;

public class Word
{
    private readonly string _originalText;
    private bool _isHidden;

    public Word(string text)
    {
        _originalText = text;
        _isHidden = false;
    }

    public bool IsHidden => _isHidden;

    public void Hide()
    {
        _isHidden = true;
    }

    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _originalText;
        }

        var builder = new StringBuilder(_originalText.Length);
        foreach (char c in _originalText)
        {
            if (char.IsLetter(c))
            {
                builder.Append('_');
            }
            else
            {
                builder.Append(c);
            }
        }
        return builder.ToString();
    }
}



