using System;
using System.Collections.Generic;
using System.Threading;

namespace Mindfulness
{
    public abstract class Activity
    {
        private readonly string _name;
        private readonly string _description;
        private int _durationSeconds;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        public void Run(PromptRepository prompts, SessionLogger logger)
        {
            ShowStartingMessage();
            _durationSeconds = PromptForDurationSeconds();
            Console.WriteLine();
            Console.WriteLine("Get ready...");
            ShowSpinner(3);
            Console.WriteLine();

            var start = DateTime.UtcNow;
            Execute(_durationSeconds, prompts);
            var end = DateTime.UtcNow;

            Console.WriteLine();
            ShowEndingMessage(start, end, logger);
        }

        protected abstract void Execute(int durationSeconds, PromptRepository prompts);

        private void ShowStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the {_name}.");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
        }

        private void ShowEndingMessage(DateTime start, DateTime end, SessionLogger logger)
        {
            Console.WriteLine("Well done!");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine($"You have completed the {_name} for {_durationSeconds} seconds.");
            ShowSpinner(3);

            try
            {
                logger?.Log(_name, _durationSeconds, start, end);
            }
            catch
            {
                // Intentionally ignore logging failures to avoid disrupting UX
            }
        }

        protected static int PromptForDurationSeconds()
        {
            while (true)
            {
                Console.Write("Enter duration in seconds: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive whole number.");
            }
        }

        protected static void ShowSpinner(int seconds)
        {
            var frames = new[] { '|', '/', '-', '\\' };
            var end = DateTime.UtcNow.AddSeconds(seconds);
            var i = 0;
            while (DateTime.UtcNow < end)
            {
                char frame = frames[i % frames.Length];
                Console.Write(frame);
                Thread.Sleep(200);
                // Erase the frame using backspace, space, backspace
                Console.Write('\b');
                Console.Write(' ');
                Console.Write('\b');
                i++;
            }
        }

        protected static void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                var text = i.ToString();
                Console.Write(text);
                Thread.Sleep(1000);
                // Erase each digit using backspace-space-backspace
                for (int k = 0; k < text.Length; k++)
                {
                    Console.Write('\b');
                    Console.Write(' ');
                    Console.Write('\b');
                }
            }
        }
    }
}


