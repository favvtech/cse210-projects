using System;

namespace Mindfulness
{
    public sealed class BodyScanActivity : Activity
    {
        public BodyScanActivity() : base(
            name: "Body Scan Activity",
            description: "This activity will guide you through a calming body scan—bringing attention to different areas and releasing tension as you go.")
        {
        }

        protected override void Execute(int durationSeconds, PromptRepository prompts)
        {
            string[] areas = new[]
            {
                "forehead",
                "eyes",
                "jaw",
                "neck and shoulders",
                "arms and hands",
                "chest",
                "stomach",
                "back",
                "hips",
                "legs",
                "feet"
            };

            var end = DateTime.UtcNow.AddSeconds(durationSeconds);
            var i = 0;
            while (DateTime.UtcNow < end)
            {
                var area = areas[i % areas.Length];
                Console.WriteLine($"Focus gently on your {area}. Soften and release any tension...");
                ShowSpinner(6);
                i++;
            }
        }
    }
}


