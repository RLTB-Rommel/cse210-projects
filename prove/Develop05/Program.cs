using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MindfulnessApp
{
    //Base class for activities
    public abstract class Activity
    {
        protected string _activityName;
        protected string _description;
        protected int _duration;
        protected int _activityCount; //creativity feature #1 track how many times the activity has been performed

        public Activity()
        {
            _activityCount = 0; //initializing activity count
        }

        public abstract void Run();
        public abstract void SaveLogs(StreamWriter writer);
        public abstract void LoadLogs(string line);
        public void StartActivity()
        {
            Console.WriteLine($"\nWelcome to the {_activityName}!\n");
            Console.WriteLine(_description);
            Console.Write("Enter the duration in seconds: ");
            _duration = int.Parse(Console.ReadLine());
            Console.WriteLine("\nGet ready to begin...");
            Pause(3);
            _activityCount++; //increment count when activity starts
        }
        public void EndActivity()
        {
            Console.WriteLine("Great job! You've completed the activity.");
            Pause(3);
        }
        protected void Pause(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\rPausing for {i} seconds...\n");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
        protected void SpinnerAnimation(int duration)
        {
            int elapsed = 0;
            while(elapsed < duration)
            {
                Console.Write("\u231B");
                Thread.Sleep(500);
                Console.Write("\r  ");
                Thread.Sleep(500);
                elapsed += 1;
            }
            Console.WriteLine();
        }
        public void ResetCount()
        {
            _activityCount = 0;
        }

    }

    //Breathing Activity class
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
        {
            _activityName = "Breathing Activity";
            _description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }
        public override void Run()
        {
            StartActivity();
            int breathingTime = _duration;

            Console.WriteLine("Starting breathing exercise...\n");
            while (breathingTime > 0)
            {
                Console.WriteLine("Breathe in...");
                SpinnerAnimation(4);
                Console.WriteLine("Breathe out...");
                SpinnerAnimation(4);
                breathingTime -= 8; //4 seconds for each breath cycle
            }
            EndActivity();
        }

            public override void SaveLogs(StreamWriter writer)
            {
                writer.WriteLine($"Breathing Activity, {_activityCount}"); // save the count for this activity
            }

        public override void LoadLogs(string line)
        {
            var parts = line.Split(',');
            if (parts[0] == "Breathing Activity")
            {
                _activityCount=int.Parse(parts[1]);//load the count from the log
            }
        }
        public int GetActivityCount()
        {
            return _activityCount;
        }
    }

    //Reflection Activity class
    public class ReflectionActivity : Activity
    {
        private List<string>_prompts = new List<string>
        {
            "Think of a time when you've stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };
         private List<string>_questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was completed?",
            "What made this different than other times?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience?",
            "How can you keep this experience in mind in the future?"
        }; 
        
        public ReflectionActivity()
        {
            _activityName ="Reflection Activity";
            _description ="This activity will help you reflect on times in your life when you have shown strength and resilience.";
        } 
        public override void Run()
        {
            StartActivity();
            Random random = new Random();
            string prompt = _prompts[random.Next(_prompts.Count)];
            Console.WriteLine(prompt);
            SpinnerAnimation(5);//pause for prompt

            int reflectionTime = _duration;
            while (reflectionTime > 0)
            {
                string question = _questions[random.Next(_questions.Count)];
                Console.WriteLine(question);
                SpinnerAnimation(4);
                reflectionTime -= 4;// 4 seconds for each question
            }
            EndActivity();
        }
        public override void SaveLogs(StreamWriter writer)
        {
            writer.WriteLine($"Reflection Activity, {_activityCount}"); // save the count for this activity
        }
        public override void LoadLogs(string line)
        {
            var parts = line.Split(',');
            if (parts[0] == "Reflection Activity")
            {
                _activityCount = int.Parse(parts[1]); // load the count from the log
            }

        } 

        public int GetActivityCount()
        {
            return _activityCount;
        }

    } 

     // Listing Activity class
     public class ListingActivity : Activity
     {
        private List<string>_prompts = new List<string>
        {
            "Who are the people that you appreciate?",
            "What are personal strength of yours?",
            "Who are the people that you helpes this week?",
            "Who are some of your personal heroes?"
        }; 

        public ListingActivity()
        {
            _activityName ="Listing Activity";
            _description ="This activity will help you reflect on good things in your life by having you list as many things as you can in a certain area.";
        } 

        public override void Run()
        {
            StartActivity();
            Random random = new Random();
            string prompt = _prompts[random.Next(_prompts.Count)];
            Console.WriteLine(prompt);
            SpinnerAnimation(5); // Pause for prompt

            Console.WriteLine("Start listing now! Press Enter after each item.");
            List<string> items = new List<string>();
            int listingTime = _duration;

            DateTime endTime = DateTime.Now.AddSeconds(listingTime);
            while (DateTime.Now < endTime)
            {
                string item = Console.ReadLine();
                items.Add(item);
            }

            Console.WriteLine($"You listed {items.Count} items.");
            EndActivity();
        }
        public override void SaveLogs(StreamWriter writer)
        {
            writer.WriteLine($"Listing Activity, {_activityCount}"); // save the count for this activity
        }
        public override void LoadLogs(string line)
        {
            var parts = line.Split(',');
            if (parts[0] == "Listing Activity")
            {
                _activityCount = int.Parse(parts[1]); // load the count from the log
            }

        }
        public int GetActivityCount()
        {
            return _activityCount;
        }


     }

    // Main program
    class Program
    {
        static BreathingActivity breathingActivity = new BreathingActivity();
        static ReflectionActivity reflectionActivity = new ReflectionActivity();
        static ListingActivity listingActivity = new ListingActivity();
        static void Main(string[] args)

        {
            LoadLogs(); // load logs at startup
            Console.OutputEncoding = System.Text.Encoding.UTF8; // enabling the unicode character for my hourglass
            
            while (true)
            {
                Console.WriteLine("\nSelect an activity:\n");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Retrieve Logs");
                Console.WriteLine("5. Reset Activity Logs");
                Console.WriteLine("6. Exit\n");
                Console.Write("Choose an option (1-6): ");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    //var breathingActivity = new BreathingActivity();
                    breathingActivity.Run();
                    SaveLogs();
                }
                else if (choice == "2")
                {
                    //var reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Run();
                    SaveLogs();
                }
                else if (choice == "3")
                {
                    //var listingActivity = new ListingActivity();
                    listingActivity.Run();
                    SaveLogs();
                }
                else if (choice == "4")
                {
                    DisplayLogs();// retrieve and display logs on request
                }
                else if (choice == "5")
                {
                    ResetActivityCount();// refresh activity
                }
                else if (choice == "6")
                {
                    break;
                }                
                else
                {
                    Console.WriteLine("Invalid choice. Please select again.");
                }
                Console.WriteLine();
            }
        }
        static void SaveLogs()
        {
            string filePath = "activity_logs.txt"; 
            try
            {
                using(StreamWriter writer = new StreamWriter(filePath, false))
                {
                    breathingActivity.SaveLogs(writer);
                    reflectionActivity.SaveLogs(writer);
                    listingActivity.SaveLogs(writer);
                }
            Console.WriteLine("Logs saved successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error saving logs:{ex.Message}");
            }

        }
        static void LoadLogs()
        {
            string filePath = "activity_logs.txt"; //creativity feature #2 uploads the logs in a text file named activity_logs
            if(File.Exists(filePath))
            {
                using  (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine())!=null)
                    {
                        breathingActivity.LoadLogs(line);
                        reflectionActivity.LoadLogs(line);
                        listingActivity.LoadLogs(line);
                    }
                }
            }

        }
        static void DisplayLogs() //creativity feature #3 retrieves the number of times the activity is done.
        {
            Console.WriteLine("\nActivity Logs:");
            Console.WriteLine($"Breathing Activity: {breathingActivity.GetActivityCount()} times");
            Console.WriteLine($"Reflection Activity: {reflectionActivity.GetActivityCount()} times");
            Console.WriteLine($"Listing Activity: {listingActivity.GetActivityCount()} times");
        }
        static void ResetActivityCount() //creativity feature #4 refresh the logged activity counts in the text file
        {
            breathingActivity.ResetCount();
            reflectionActivity.ResetCount();
            listingActivity.ResetCount();
        }
    }
}