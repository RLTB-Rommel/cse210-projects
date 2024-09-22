using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello Prep4 World!");
        Console.WriteLine("List Assignment");
        Console.WriteLine();

        List<int> numbers = new List<int>();
        int input;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers from user
        do
        {
            Console.Write("Enter number: ");
            input = Convert.ToInt32(Console.ReadLine());

            if (input != 0)
            {
                numbers.Add(input);
            }

        } while (input != 0); // End of do-while loop

        // Check if any numbers were entered
        if (numbers.Count > 0)
        {
            // Calculate sum
            int sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }

            // Calculate average
            double average = (double)sum / numbers.Count;

            // Find maximum
            int max = numbers[0];
            foreach (var number in numbers)
            {
                if (number > max)
                {
                    max = number;
                }
            }

            // Find smallest positive number
            int? smallestPositive = null;
            foreach (var number in numbers)
            {
                if (number > 0 && (smallestPositive == null || number < smallestPositive))
                {
                    smallestPositive = number;
                }
            }

            // Sort the list
            numbers.Sort();

            // Display results
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            Console.WriteLine("The sorted list is:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}