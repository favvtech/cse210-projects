using System;
using System.Collections.Generic;

namespace Mindfulness
{
    public sealed class ListingActivity : Activity
    {
        public ListingActivity() : base(
            name: "Listing Activity",
            description: "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
        }

        protected override void Execute(int durationSeconds, PromptRepository prompts)
        {
            var prompt = prompts.GetRandomListingPrompt();
            Console.WriteLine("List as many responses you can to the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("You may begin in:");
            ShowCountdown(5);
            Console.WriteLine();

            var responses = new List<string>();
            var end = DateTime.UtcNow.AddSeconds(durationSeconds);
            while (DateTime.UtcNow < end)
            {
                if (Console.KeyAvailable)
                {
                    Console.Write("> ");
                    var line = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        responses.Add(line.Trim());
                    }
                }
                else
                {
                    // Light heartbeat to keep UI responsive
                    System.Threading.Thread.Sleep(50);
                }
            }

            Console.WriteLine($"You listed {responses.Count} items!");
        }
    }
}


