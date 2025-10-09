using System;
using Mindfulness;

class Program
{
    static void Main(string[] args)
    {
        // Enhancements beyond core requirements:
        // - Session logging to CSV (Documents/Mindfulness/sessions.csv)
        // - Session-unique, no-repeat prompt/question selection until all are used
        // - Added a fourth activity: Body Scan Activity (guided attention through body areas)
        var prompts = new PromptRepository();
        var logger = new SessionLogger(PathCombine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mindfulness", "sessions.csv"));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Start body scan activity (new)");
            Console.WriteLine("  5. Quit");
            Console.Write("Select a choice from the menu: ");
            var choice = Console.ReadLine();

            Activity activity = null;
            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": activity = new BodyScanActivity(); break;
                case "5": return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to continue...");
                    Console.ReadLine();
                    continue;
            }

            activity.Run(prompts, logger);
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }

    private static string PathCombine(string a, string b, string c)
    {
        return System.IO.Path.Combine(a, b, c);
    }
}