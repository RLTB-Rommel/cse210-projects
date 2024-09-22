using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello Prep3 World!");
        Console.WriteLine("Guess the Number Assignment.");
        Console.WriteLine("Stretch challenge included");
        Console.WriteLine();
    
        //Using a random number
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        
        // Initial prompt
        Console.WriteLine("A random number is generated, please guess it.");

        int userGuess = -1; // Initialize the user's guess 
         
        // Loop until the user guesses the magic number
        while (userGuess != magicNumber)
        {
            // Ask for the user's guess
            Console.Write("What is your guess? ");
            while (!int.TryParse(Console.ReadLine(), out userGuess))
            {
                Console.Write("Please enter a valid integer for your guess: ");
            }
        
            // Check the guess and provide feedback
            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}