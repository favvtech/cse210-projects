using System;
using System.IO;

class Program
{
	// Exceeds core requirements:
	// - CSV and JSON storage supported: save/load auto-detected by filename extension (.csv or .json).
	//   CSV implements correct escaping (quotes doubled, fields quoted) so files open cleanly in Excel.
	//   JSON uses System.Text.Json with indented output for readability and easy interop.
	// - Default filename when saving: pressing Enter saves to "journal.txt".
	// - Saves/loads relative paths in the project root (not bin), printed as absolute paths for clarity.
	// - Flexible commands: accepts numeric selections or words (e.g., "Write", "Display").
	// - Robust load handling: friendly error message instead of crashing if file is missing/invalid.
	// - After Load, the app prints the number of entries and auto-displays them.
	// - Display formatting: shows "Date: ... - Prompt: ..." followed by the response on the next line.
	static void Main(string[] args)
	{
		Journal journal = new Journal();
		PromptGenerator promptGenerator = new PromptGenerator();
		bool running = true;

		Console.WriteLine("Welcome to the Journal Program!");

		while (running)
		{
			Console.WriteLine("Please select one of the following choices:");
			Console.WriteLine("1. Write");
			Console.WriteLine("2. Display");
			Console.WriteLine("3. Load");
			Console.WriteLine("4. Save");
			Console.WriteLine("5. Quit");
			Console.Write("What would you like to do? ");

			string choice = Console.ReadLine();
			Console.WriteLine();

			switch (choice)
			{
				case "1":
				case "Write":
				case "write":
					HandleWrite(journal, promptGenerator);
					break;

				case "2":
				case "Display":
				case "display":
					HandleDisplay(journal);
					break;

				case "3":
				case "Load":
				case "load":
					HandleLoad(journal);
					break;

				case "4":
				case "Save":
				case "save":
					HandleSave(journal);
					break;

				case "5":
				case "Quit":
				case "quit":
					running = false;
					break;

				default:
					Console.WriteLine("Please choose 1-5.");
					break;
			}

			if (running)
			{
				Console.WriteLine();
			}
		}
	}

	private static string ResolveToProjectRoot(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath))
		{
			return filePath;
		}
		if (Path.IsPathRooted(filePath))
		{
			return Path.GetFullPath(filePath);
		}
		string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
		string combined = Path.Combine(projectRoot, filePath);
		return Path.GetFullPath(combined);
	}

	static void HandleWrite(Journal journal, PromptGenerator generator)
	{
		string prompt = generator.GetRandomPrompt();
		Console.WriteLine(prompt);
		Console.Write("> ");
		string response = Console.ReadLine() ?? string.Empty;
		string date = DateTime.Now.ToString("M/d/yyyy");
		journal.AddEntry(new Entry(date, prompt, response));
	}

	static void HandleDisplay(Journal journal)
	{
		foreach (Entry entry in journal.GetEntries())
		{
			Console.WriteLine(entry.ToString());
			Console.WriteLine();
		}
	}

	static void HandleSave(Journal journal)
	{
		Console.Write("Enter filename to save (.txt default; use .csv or .json to choose format): ");
		string filePath = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(filePath))
		{
			filePath = "journal.txt";
		}
		string fullPath = ResolveToProjectRoot(filePath);
        journal.SaveToFile(fullPath);
        Console.WriteLine($"Saved to {fullPath}");
	}

	static void HandleLoad(Journal journal)
	{
		Console.Write("Enter filename to load (.txt, .csv, or .json): ");
		string filePath = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(filePath))
		{
			Console.WriteLine("Load cancelled.");
			return;
		}
		try
		{
			string fullPath = ResolveToProjectRoot(filePath);
			journal.LoadFromFile(fullPath);
			int count = 0;
			foreach (Entry _ in journal.GetEntries()) { count++; }
			Console.WriteLine($"Loaded from {fullPath} ({count} entries)");
			if (count > 0)
			{
				Console.WriteLine();
				HandleDisplay(journal);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error loading file: {ex.Message}");
		}
	}
}