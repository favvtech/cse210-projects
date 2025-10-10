using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override bool IsComplete => false;

    public override int RecordEvent()
    {
        return Points;
    }

    public override string GetStatusText()
    {
        return $"[ ] {Name} ({Description})";
    }

    public override string Serialize()
    {
        return string.Join("|", new string[] { "EternalGoal", Name, Description, Points.ToString() });
    }

    public static EternalGoal Deserialize(string[] parts)
    {
        // parts: [Type, Name, Description, Points]
        string name = parts[1];
        string description = parts[2];
        int points = int.Parse(parts[3]);
        return new EternalGoal(name, description, points);
    }
}


