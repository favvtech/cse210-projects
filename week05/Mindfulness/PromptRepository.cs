using System;
using System.Collections.Generic;

namespace Mindfulness
{
    public sealed class PromptRepository
    {
        private readonly Random _random = new Random();

        private readonly List<string> _reflectionPrompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private readonly List<string> _reflectionQuestions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private readonly List<string> _listingPrompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        // Session-unique queues (refilled with a fresh shuffle when empty)
        private Queue<string> _reflectionPromptQueue;
        private Queue<string> _reflectionQuestionQueue;
        private Queue<string> _listingPromptQueue;

        public PromptRepository()
        {
            _reflectionPromptQueue = BuildQueue(_reflectionPrompts);
            _reflectionQuestionQueue = BuildQueue(_reflectionQuestions);
            _listingPromptQueue = BuildQueue(_listingPrompts);
        }

        public string GetRandomReflectionPrompt() => NextUnique(ref _reflectionPromptQueue, _reflectionPrompts);
        public string GetRandomReflectionQuestion() => NextUnique(ref _reflectionQuestionQueue, _reflectionQuestions);
        public string GetRandomListingPrompt() => NextUnique(ref _listingPromptQueue, _listingPrompts);

        private string NextUnique(ref Queue<string> queue, List<string> seed)
        {
            if (queue.Count == 0)
            {
                queue = BuildQueue(seed);
            }
            return queue.Count > 0 ? queue.Dequeue() : string.Empty;
        }

        private Queue<string> BuildQueue(List<string> source)
        {
            var copy = new List<string>(source);
            // Fisher-Yates shuffle
            for (int i = copy.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                var tmp = copy[i];
                copy[i] = copy[j];
                copy[j] = tmp;
            }
            return new Queue<string>(copy);
        }
    }
}


