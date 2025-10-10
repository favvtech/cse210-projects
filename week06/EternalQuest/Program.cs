/*
 * Exceeded Requirements (Creativity):
 * - Levels: Level increases every 1000 points. Current level and next threshold displayed.
 * - Badges: Earned for first event ("Getting Started"), completing a checklist goal ("Checklist Champion"),
 *   and upon leveling up (e.g., "Level 2 Achieved").
 * - Persistence: Gamification metadata (events count and badges) is saved/loaded with goals.
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine($"You have {manager.Score} points. Level {manager.Level} (next at {manager.NextLevelAt}).");
            if (manager.Badges.Count > 0)
            {
                Console.WriteLine("Badges: " + string.Join(", ", manager.Badges));
            }
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            if (input == "1")
            {
                CreateGoalFlow(manager);
            }
            else if (input == "2")
            {
                manager.ListGoals();
            }
            else if (input == "3")
            {
                Console.Write("Enter filename to save (default: goals.txt): ");
                string file = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(file))
                {
                    file = "goals.txt";
                }
                manager.SaveToFile(file);
                Console.WriteLine($"Saved {manager.Goals.Count} goal(s).");
            }
            else if (input == "4")
            {
                Console.Write("Enter filename to load (default: goals.txt): ");
                string file = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(file))
                {
                    file = "goals.txt";
                }
                manager.LoadFromFile(file);
                Console.WriteLine("Current goals:");
                manager.ListGoals();
            }
            else if (input == "5")
            {
                if (manager.Goals.Count == 0)
                {
                    Console.WriteLine("No goals to record.");
                }
                else
                {
                    Console.WriteLine("Which goal did you accomplish?");
                    for (int i = 0; i < manager.Goals.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {manager.Goals[i].Name}");
                    }
                    Console.Write("Select a goal number: ");
                    if (int.TryParse(Console.ReadLine(), out int idx))
                    {
                        int points = manager.RecordEvent(idx - 1);
                        Console.WriteLine($"Congratulations! You have earned {points} points!");
                        Console.WriteLine($"You now have {manager.Score} points.");
                    }
                }
            }
            else if (input == "6")
            {
                break;
            }
        }

    }

    static void CreateGoalFlow(GoalManager manager)
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string type = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? string.Empty;
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? string.Empty;
        Console.Write("What is the amount of points associated with this goal? ");
        int points = ReadInt();

        if (type == "1")
        {
            manager.AddGoal(new SimpleGoal(name, description, points));
        }
        else if (type == "2")
        {
            manager.AddGoal(new EternalGoal(name, description, points));
        }
        else if (type == "3")
        {
            Console.Write("How many times does this goal need to be accomplished for a bonus? ");
            int target = ReadInt();
            Console.Write("What is the bonus for accomplishing it that many times? ");
            int bonus = ReadInt();
            manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
        }
    }

    static int ReadInt()
    {
        string? s = Console.ReadLine();
        if (int.TryParse(s, out int result))
        {
            return result;
        }
        return 0;
    }
}