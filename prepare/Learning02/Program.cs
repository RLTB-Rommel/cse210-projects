using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job
        {
            JobTitle = "Software Developer",
            Company = "Google",
            StartYear = 2015,
            EndYear = 2019
        };

        Job job2 = new Job
        {
            JobTitle = "Network Administrator",
            Company = "Microsoft",
            StartYear = 2020,
            EndYear = 2023
        };

        Resume myResume = new Resume
        {
            Name = "Rommel F. Aunario"
        };

        myResume.Jobs.Add(job1);
        myResume.Jobs.Add(job2);

        myResume.Display();
    }
}