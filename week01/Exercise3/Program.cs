using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        string playAgain = "yes";
        
        while (playAgain == "yes")
        {
            int magicNumber = randomGenerator.Next(1, 101);

            int guess;
            int guessCount = 0;
            do
            {
                Console.Write("What is your guess? ");
                string guessInput = Console.ReadLine();
                guess = int.Parse(guessInput);
                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            } while (guess != magicNumber);

            Console.WriteLine($"It took you {guessCount} guesses!");
            Console.Write("Do you want to play again? ");
            playAgain = Console.ReadLine();
        }
    }
}