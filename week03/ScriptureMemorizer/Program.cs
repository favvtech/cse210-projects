using System;

class Program
{
    static void Main(string[] args)
    {
        // Exceeding requirements :
        // - Have your program work with a library of scriptures rather than a single one. Choose scriptures at random to present to the user, multiple-scripture library with random selection.
        // - Progressive difficulty: hide-only-visible-words per round.
        // - Have the program to load scriptures from a files, file-based loading of scriptures (3): implemented via scriptures.json; falls back to built-ins if file missing.

        var scriptures = ScriptureFileLoader.LoadFromJson("scriptures.json");
        if (scriptures == null || scriptures.Count == 0)
        {
            scriptures = new System.Collections.Generic.List<Scripture>
            {
                new Scripture(new Reference("John", 3, 16),
                    "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
                new Scripture(new Reference("Proverbs", 3, 5, 6),
                    "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."),
                new Scripture(new Reference("Psalm", 23, 1),
                    "The Lord is my shepherd; I shall not want.")
            };
        }

        var random = new Random();
        var scripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to hide words, or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input != null && input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            // Hide a few random visible words each time (e.g., 3)
            scripture.HideRandomVisibleWords(3);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }
        }
    }
}