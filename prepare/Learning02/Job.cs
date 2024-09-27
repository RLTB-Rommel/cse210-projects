using System;

public class Job
{
    public string JobTitle { get; set; }
    public string Company { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }

    public void Display()
    {
        Console.WriteLine($"{JobTitle} at {Company} ({StartYear} - {EndYear})");
    }
}
