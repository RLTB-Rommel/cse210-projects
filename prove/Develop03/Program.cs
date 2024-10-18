using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorization
{
    public class ScriptureReference
    {
        private string _reference;

        public string Reference
        {
            get => _reference;
            private set => _reference = value;
        }

        public ScriptureReference(string reference)
        {
            Reference = reference;
        }

        public ScriptureReference(string book, int chapter, int verse)
        {
            Reference = $"{book} {chapter}:{verse}";
        }

        public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
        {
            Reference = $"{book} {chapter}:{startVerse}-{endVerse}";
        }
    }

    public class Word
    {
        private string _text;
        private bool _isHidden;

        public string Text
        {
            get => _text;
            private set => _text = value;
        }

        public bool IsHidden
        {
            get => _isHidden;
            set => _isHidden = value;
        }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public override string ToString()
        {
            return IsHidden ? "_____" : Text;
        }
    }

    public class Scripture
    {
        private ScriptureReference _reference;
        private List<Word> _words;

        public ScriptureReference Reference
        {
            get => _reference;
            private set => _reference = value;
        }

        public List<Word> Words
        {
            get => _words;
            private set => _words = value;
        }

        public Scripture(ScriptureReference reference, string text)
        {
            Reference = reference;
            Words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        public void HideRandomWord(Random random)
        {
            if (Words.All(w => w.IsHidden)) return;

            int visibleCount = Words.Count(w => !w.IsHidden);
            if (visibleCount == 0) return;

            int index;
            do
            {
                index = random.Next(Words.Count);
            } while (Words[index].IsHidden);

            Words[index].IsHidden = true;
        }

        public override string ToString()
        {
            return $"{Reference.Reference}\n" + string.Join(" ", Words);
        }

        public bool AllWordsHidden()
        {
            return Words.All(w => w.IsHidden);
        }
    }

    class Program
    {
        private static List<Scripture> _scriptures = new List<Scripture>();
        private static List<Scripture> _usedScriptures = new List<Scripture>();

        static void Main(string[] args)
        {
            string filePath = "scriptures.txt";
            LoadScripturesFromFile(filePath);

            // Check if there are scriptures loaded
            if (_scriptures.Count == 0)
            {
                Console.WriteLine("No scriptures found. Exiting the program.");
                return; // Exit if no scriptures are available
            }

            Random random = new Random();
            bool quit = false;

            while (!quit)
            {
                if (_scriptures.Count == 0) // Check if all scriptures have been used
                {
                    Console.WriteLine("All scriptures have been processed. Exiting the program.");
                    break; // Exit the main loop
                }

                var scripture = GetRandomScripture(random);
                _usedScriptures.Add(scripture); // Add to used scriptures
                _scriptures.Remove(scripture); // Remove from available scriptures

                bool scriptureComplete = false;

                while (!scriptureComplete)
                {
                    Console.Clear();
                    Console.WriteLine(scripture);

                    int wordsLeft = scripture.Words.Count(w => !w.IsHidden);
                    Console.WriteLine($"\nWords left to hide: {wordsLeft}");

                    Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
                    string input = Console.ReadLine();
                    if (input?.ToLower() == "quit")
                    {
                        quit = true;
                        break;
                    }
                    else
                    {
                        scripture.HideRandomWord(random); // Hide a word
                        wordsLeft = scripture.Words.Count(w => !w.IsHidden); // Update wordsLeft after hiding

                        // Debugging: Show the current state of words
                        Console.WriteLine("Current words state:");
                        foreach (var word in scripture.Words)
                        {
                            Console.WriteLine(word.ToString()); // This will show hidden or visible words
                        }

                        if (wordsLeft == 0) // Check if no words are left
                        {
                            Console.Clear(); // Clear the console before showing the congratulations message
                            Console.WriteLine("\nCongratulations! All words are hidden!"); // Display message
                            scriptureComplete = true; // Mark scripture as complete
                        }
                    }
                }

                if (scriptureComplete)
                {
                    while (true)
                    {
                        Console.WriteLine("\nDo you want another scripture to memorize? (yes/no)");
                        string response = Console.ReadLine();
                        if (response?.ToLower() == "yes")
                        {
                            break; // Get a new scripture
                        }
                        else if (response?.ToLower() == "no")
                        {
                            quit = true; // Exit the main loop
                            break; // Exit the confirmation loop
                        }
                        else
                        {
                            Console.WriteLine("Please enter 'yes' or 'no'.");
                        }
                    }
                }
            }
        }

        private static void LoadScripturesFromFile(string filePath)
        {
            Console.WriteLine($"Checking for file: {filePath}");
            Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

            if (File.Exists(filePath))
            {
                Console.WriteLine("File found. Reading lines...");
                try
                {
                    var lines = File.ReadAllLines(filePath);
                    Console.WriteLine($"Number of lines read: {lines.Length}");

                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            var reference = new ScriptureReference(parts[0]);
                            var text = parts[1];
                            _scriptures.Add(new Scripture(reference, text));
                        }
                        else
                        {
                            Console.WriteLine($"Invalid line format: {line}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: The file {filePath} does not exist.");
            }
        }

        private static Scripture GetRandomScripture(Random random)
        {
            return _scriptures[random.Next(_scriptures.Count)];
        }
    }
}