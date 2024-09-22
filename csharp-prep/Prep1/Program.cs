using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello Prep1 World!");
        Console.WriteLine();  // added for in between space        
        // prompt for the user's first name
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();
        
        // prompt for the user's last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();
        Console.WriteLine(); //added for in between space
        // display combined results
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
        Console.WriteLine();  // added for bottom space     
        
    }
}

