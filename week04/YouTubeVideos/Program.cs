using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to hold videos
        List<Video> videos = new List<Video>();

        // Create 4 videos with sample data
        Video video1 = new Video("How to Code in C#", "TechTutor", 600);
        Video video2 = new Video("Cooking Italian Pasta", "ChefMaria", 900);
        Video video3 = new Video("Garden Tips for Beginners", "GreenThumb", 450);
        Video video4 = new Video("Travel Guide to Japan", "Wanderlust", 1200);

        // Add comments to video1
        video1.AddComment(new Comment("JohnDoe", "Great tutorial! Very helpful."));
        video1.AddComment(new Comment("JaneSmith", "I learned so much from this video."));
        video1.AddComment(new Comment("CodeNewbie", "Perfect for beginners like me."));
        video1.AddComment(new Comment("DevMaster", "Excellent explanation of concepts."));

        // Add comments to video2
        video2.AddComment(new Comment("FoodLover", "The pasta looks amazing!"));
        video2.AddComment(new Comment("CookingFan", "I tried this recipe and it was delicious."));
        video2.AddComment(new Comment("ItalianChef", "Authentic Italian technique shown here."));
        video2.AddComment(new Comment("KitchenPro", "Great tips for perfect pasta."));

        // Add comments to video3
        video3.AddComment(new Comment("PlantParent", "These tips really work!"));
        video3.AddComment(new Comment("GardenNewbie", "Perfect for someone starting out."));
        video3.AddComment(new Comment("GreenFingers", "Love the practical advice."));
        video3.AddComment(new Comment("NatureLover", "My plants are thriving now."));

        // Add comments to video4
        video4.AddComment(new Comment("TravelBug", "Japan is on my bucket list!"));
        video4.AddComment(new Comment("Wanderer", "Great travel tips and insights."));
        video4.AddComment(new Comment("CultureSeeker", "Love learning about Japanese culture."));
        video4.AddComment(new Comment("AdventureSeeker", "This makes me want to book a trip!"));

        // Add videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        // Iterate through the list and display video information
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  - {comment.GetCommenterName()}: {comment.GetCommentText()}");
            }
            Console.WriteLine();
        }
    }
}