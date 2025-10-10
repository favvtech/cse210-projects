using System;

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override bool IsComplete => _isComplete;

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            return 0;
        }

        _isComplete = true;
        return Points;
    }

    public override string GetStatusText()
    {
        string box = _isComplete ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description})";
    }

    public override string Serialize()
    {
        return string.Join("|", new string[] { "SimpleGoal", Name, Description, Points.ToString(), _isComplete ? "1" : "0" });
    }

    public static SimpleGoal Deserialize(string[] parts)
    {
        // parts: [Type, Name, Description, Points, IsComplete]
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);
        bool isComplete = parts.Length > 4 && parts[4] == "1";
        return new SimpleGoal(name, description, points, isComplete);
    }
}


