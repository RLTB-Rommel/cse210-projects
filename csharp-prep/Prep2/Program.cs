using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello Prep2 World!");
        Console.WriteLine("Stretch Challenge included.");
        Console.WriteLine(); //added for in between space

        //prompt for grade percentage
        Console.Write("Please enter your grade percentage: ");
        int percentage = int.Parse(Console.ReadLine());

        //Variable to hold the letter grade
        char letter;

        //Variable to hold the sign for stretch challenge
        string sign = "";

        // Determine the letter grade
        if (percentage >= 90)
        {
            letter = 'A';
        }

        else if(percentage>= 80)
        {
            letter = 'B';
        }

        else if(percentage>= 70)
        {
            letter = 'C';
        }
        else if(percentage>= 60)
        {
            letter = 'D';
        }

        else
        {
            letter = 'F';
        }
        //Determine the sign for A, B, C, D for stretch challenge
        //Exclude letters A and F for the sign determination

        if (letter != 'A' && letter !='F')

        { 
            int lastDigit = percentage % 10; // Get the last digit
            if (lastDigit>= 7)
            {
                sign ="+";
            }

            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        //Handle Special Cases for A, F
        if (letter == 'A' && sign =="+")
        {
            sign = ""; // no A+
        }
        else if (letter == 'F')

        {
            sign = ""; //no F+ or F-
        }

        //Print the letter grade

        Console.WriteLine($"Your letter grade is: {letter}{sign}");

        // Check if the user passed the subject

        if(percentage >= 70)

        {
            Console.WriteLine("Congratulations! You passed the course!");
        }

        else
        {
            Console.WriteLine("Don't worry, but keep trying! You'll get it next time.");
        }

    }
}