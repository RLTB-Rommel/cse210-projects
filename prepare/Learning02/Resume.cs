using System;
using System.Collections.Generic;

public class Resume
{
    public string Name { get; set; }
    public List<Job> Jobs { get; set; } = new List<Job>();

    public void Display()
    {
        Console.WriteLine();
        Console.WriteLine($"Resume for: {Name}");
        Console.WriteLine("Work Experience:");
        foreach (var job in Jobs)
        {
            job.Display();
        }
        Console.WriteLine();
    }
}
