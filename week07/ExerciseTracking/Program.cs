using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create activities
            var activities = new List<Activity>
            {
                new Running(new DateTime(2022, 11, 3), 30, 3.0), // 30 min, 3.0 miles
                new Cycling(new DateTime(2022, 11, 3), 30, 6.0), // 30 min, 6.0 mph
                new Swimming(new DateTime(2022, 11, 3), 30, 60)  // 30 min, 60 laps
            };

            // Display summaries
            Console.WriteLine("Exercise Tracking Summary");
            Console.WriteLine("========================");
            Console.WriteLine();

            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}