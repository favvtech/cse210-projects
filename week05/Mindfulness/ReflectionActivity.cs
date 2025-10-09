using System;

namespace Mindfulness
{
    public sealed class ReflectionActivity : Activity
    {
        public ReflectionActivity() : base(
            name: "Reflection Activity",
            description: "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
        }

        protected override void Execute(int durationSeconds, PromptRepository prompts)
        {
            var prompt = prompts.GetRandomReflectionPrompt();
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine("When you have something in mind, press Enter to continue.");
            Console.ReadLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
            Console.WriteLine();

            var end = DateTime.UtcNow.AddSeconds(durationSeconds);
            while (DateTime.UtcNow < end)
            {
                var question = prompts.GetRandomReflectionQuestion();
                Console.WriteLine($"> {question}");
                ShowSpinner(6);
            }
        }
    }
}


