using System;
using System.Collections.Generic;
using System.IO;

class GoalManager
{
    private readonly List<Goal> _goals = new List<Goal>();
    private int _score;
    private readonly HashSet<string> _badges = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private int _eventsRecorded;

    public int Score => _score;
    public IReadOnlyList<Goal> Goals => _goals.AsReadOnly();
    public int Level => (_score / 1000) + 1;
    public int NextLevelAt => (Level * 1000);
    public IReadOnlyCollection<string> Badges => _badges;
    public int EventsRecorded => _eventsRecorded;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public int RecordEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= _goals.Count)
        {
            return 0;
        }

        int currentLevel = Level;
        Goal goal = _goals[goalIndex];
        bool wasCompleteBefore = goal.IsComplete;
        int points = goal.RecordEvent();
        int newScore = _score + points;
        int newLevel = (newScore / 1000) + 1;

        if (points > 0 && _eventsRecorded == 0)
        {
            _badges.Add("Getting Started");
        }

        // Checklist completion badge
        if (!wasCompleteBefore && goal is ChecklistGoal && goal.IsComplete)
        {
            _badges.Add("Checklist Champion");
        }

        // Level up badge
        if (newLevel > currentLevel)
        {
            _badges.Add($"Level {newLevel} Achieved");
        }

        _score = newScore;
        if (points > 0)
        {
            _eventsRecorded += 1;
        }
        return points;
    }

    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetStatusText()}");
        }
    }

    public void SaveToFile(string filePath)
    {
        try
        {
            string fullPath = Path.GetFullPath(filePath);
            string? dir = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(_score);
                // meta: events and badges
                writer.WriteLine("#META|" + _eventsRecorded + "|" + string.Join(",", _badges));
                foreach (var goal in _goals)
                {
                    writer.WriteLine(goal.Serialize());
                }
            }

            Console.WriteLine($"Saved to: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to save file: " + ex.Message);
        }
    }

    public void LoadFromFile(string filePath)
    {
        try
        {
            string fullPath = Path.GetFullPath(filePath);
            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File not found: " + fullPath);
                return;
            }

            _goals.Clear();
            _badges.Clear();
            string[] lines = File.ReadAllLines(fullPath);
            if (lines.Length == 0)
            {
                _score = 0;
                return;
            }

            _score = int.TryParse(lines[0], out int s) ? s : 0;
            int startIndex = 1;
            if (lines.Length > 1 && lines[1].StartsWith("#META|", StringComparison.Ordinal))
            {
                string[] meta = lines[1].Split('|');
                if (meta.Length >= 3)
                {
                    _eventsRecorded = int.TryParse(meta[1], out int ev) ? ev : 0;
                    if (!string.IsNullOrWhiteSpace(meta[2]))
                    {
                        foreach (var b in meta[2].Split(',', StringSplitOptions.RemoveEmptyEntries))
                        {
                            _badges.Add(b.Trim());
                        }
                    }
                }
                startIndex = 2;
            }

            for (int i = startIndex; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts.Length == 0) continue;
                switch (parts[0])
                {
                    case "SimpleGoal":
                        _goals.Add(SimpleGoal.Deserialize(parts));
                        break;
                    case "EternalGoal":
                        _goals.Add(EternalGoal.Deserialize(parts));
                        break;
                    case "ChecklistGoal":
                        _goals.Add(ChecklistGoal.Deserialize(parts));
                        break;
                }
            }

            Console.WriteLine("Loaded from: " + fullPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load file: " + ex.Message);
        }
    }
}


