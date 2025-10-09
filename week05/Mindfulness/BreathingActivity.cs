using System;

namespace Mindfulness
{
    public sealed class BreathingActivity : Activity
    {
        public BreathingActivity() : base(
            name: "Breathing Activity",
            description: "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.")
        {
        }

        protected override void Execute(int durationSeconds, PromptRepository prompts)
        {
            var end = DateTime.UtcNow.AddSeconds(durationSeconds);
            var inhale = true;
            while (DateTime.UtcNow < end)
            {
                if (inhale)
                {
                    Console.WriteLine("Breathe in...");
                }
                else
                {
                    Console.WriteLine("Now breathe out...");
                }
                ShowCountdown(4);
                inhale = !inhale;
            }
        }
    }
}


