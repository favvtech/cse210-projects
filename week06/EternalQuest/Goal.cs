using System;

abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract bool IsComplete { get; }
    public abstract int RecordEvent();
    public abstract string GetStatusText();
    public abstract string Serialize();
}


