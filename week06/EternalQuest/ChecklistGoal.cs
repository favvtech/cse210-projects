using System;

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, int currentCount = 0)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = currentCount;
    }

    public override bool IsComplete => _currentCount >= _targetCount;

    public override int RecordEvent()
    {
        if (IsComplete)
        {
            return 0;
        }

        _currentCount += 1;
        int awarded = Points;
        if (_currentCount == _targetCount)
        {
            awarded += _bonusPoints;
        }
        return awarded;
    }

    public override string GetStatusText()
    {
        string box = IsComplete ? "[X]" : "[ ]";
        return $"{box} {Name} ({Description}) -- Currently completed: {_currentCount}/{_targetCount}";
    }

    public override string Serialize()
    {
        return string.Join("|", new string[] { "ChecklistGoal", Name, Description, Points.ToString(), _targetCount.ToString(), _bonusPoints.ToString(), _currentCount.ToString() });
    }

    public static ChecklistGoal Deserialize(string[] parts)
    {
        // parts: [Type, Name, Description, Points, Target, Bonus, Current]
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);
        int target = int.Parse(parts[4]);
        int bonus = int.Parse(parts[5]);
        int current = parts.Length > 6 ? int.Parse(parts[6]) : 0;
        return new ChecklistGoal(name, description, points, target, bonus, current);
    }
}


