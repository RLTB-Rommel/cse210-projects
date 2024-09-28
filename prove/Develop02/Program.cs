using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Newtonsoft.Json;

class Program
{
    static void Main()

    {
        Journal journal = new Journal(); //create a new instance of the Journal class
        bool running = true; //control the main loop

        Console.WriteLine();
        Console.WriteLine("Welcome to My Journal Program.");
        Console.WriteLine("Please feel to share, record and retrieve your experiences this day.");
        Console.WriteLine();
        while (running)
        {
            //Display the menu options to the user

            Console.WriteLine("Choose any option:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal.");
            Console.WriteLine("3. Save journal to file.");
            Console.WriteLine("4. Load journal from file.");
            Console.WriteLine("5. Exit");
            string choice = Console.ReadLine();
        
            //switch statement to handle user choices

            switch (choice)
            {
                case "1":
                    journal.AddEntry(); //Add a new journal entry
                    break;

                case "2":
                    journal.DisplayEntries(); //Display all journal entries
                    break;

                case "3":
                    Console.Write("Enter filename: "); 
                    string saveFile = Console.ReadLine(); // Get filename for saving
                    journal.SaveToFile(saveFile); //Save entries to file
                    break;
            
                case "4":
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine(); // Get filename for loading
                    journal.LoadFromFile(loadFile); //Load entries from file
                    break;
            
                case "5":

                    running = false; // Exit the loop
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again."); // handle an invalid input
                    break;  
            }
        
        }
    }
}

class Journal
{
    private List<Entry> entries = new List<Entry>(); // List to store journal entries
    private static readonly string[] prompts  = // predefined prompt for journal entries

    {

        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"

    };

    public void AddEntry()
    {
        Random rand = new Random(); // create a random number generator
        string prompt = prompts[rand.Next(prompts.Length)]; // select a random prompt
        Console.WriteLine(prompt); // display the prompt to the user
        string response = Console.ReadLine(); // get user response

        // Get additional information: mood and reminder --> Exceed Requirement Task # 1
        Console.Write("How do you feel today (e.g., happy, sad: )");
        string mood = Console.ReadLine();
        Console.Write("Enter a reminder for your next journal entry: ");
        string reminder = Console.ReadLine();
        
        // add the new entry to the list with current date and time
        entries.Add(new Entry(prompt,response,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),mood, reminder));
    }
    public void DisplayEntries()
    {
        // iterate through each entry and display it
        foreach (var entry in entries)
    
        {
            Console.WriteLine($"{entry.Date} - {entry.Prompt}: {entry.Response} | Mood: {entry.Mood} | Reminder: {entry.Reminder}");
        }

    }

    public void SaveToFile(string filename)
    {
        
        // Serialize entries to the JSON format --> Exceed Requirement Task # 2
        string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
        File.WriteAllText(filename, json);
        Console.WriteLine("Journal saved as JSON");

    }    
        
    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);
            Console.WriteLine("JSON Loaded: " + json); // Display loaded JSON for debugging

            try
            {
                entries = JsonConvert.DeserializeObject<List<Entry>>(json); 
                Console.WriteLine("Journal loaded from JSON");
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error deserializing JSON: " + ex.Message); // Error handling
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
public void SaveToCsv(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            // Write header
            writer.WriteLine("Date,Prompt,Response,Mood,Reminder");
            foreach (var entry in entries)
            {
                // Format entry for CSV and escape quotes
                writer.WriteLine($"\"{entry.Date}\",\"{EscapeCsv(entry.Prompt)}\",\"{EscapeCsv(entry.Response)}\",\"{EscapeCsv(entry.Mood)}\",\"{EscapeCsv(entry.Reminder)}\"");
            }
        }
        Console.WriteLine("Journal saved as CSV");
    }

    public void LoadFromCsv(string filename)
    {
        if (File.Exists(filename))
        {
            entries.Clear(); // Clear existing entries
            using (StreamReader reader = new StreamReader(filename))
            {
                // Skip header
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var parts = line.Split(new[] { ',' }, StringSplitOptions.None);

                    if (parts.Length == 5) // Ensure there are enough parts
                    {
                        entries.Add(new Entry(parts[1].Trim('"'), parts[2].Trim('"'), parts[0].Trim('"'), parts[3].Trim('"'), parts[4].Trim('"')));
                    }
                }
            }
            Console.WriteLine("Journal loaded from CSV");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    private string EscapeCsv(string value)
    {
        if (value.Contains("\""))
        {
            value = value.Replace("\"", "\"\"");
        }
        return value;
    }
}

class Entry
{
    public string Prompt { get; } // Prompt for the entry
    public string Response { get; } // User's response
    public string Date { get; } // Date of the entry
    public string Mood { get; } // Additional field for mood
    public string Reminder { get; } // Additional field for reminder

    // Constructor to initialize a new entry
    public Entry(string prompt, string response, string date, string mood, string reminder)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
        Mood = mood;
        Reminder = reminder;
    }
}
