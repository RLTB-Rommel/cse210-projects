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

            int index;
            do
            {
                index = random.Next(Words.Count);
            } while (Words[index].IsHidden);

            Words[index].IsHidden = true;
        }

        public override string ToString()
        {
            return $"{Reference.Reference}: " + string.Join(" ", Words);
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

            if (_scriptures.Count == 0)
            {
                Console.WriteLine("No scriptures found. Exiting the program.");
                return; // Exit if no scriptures are available
            }

            Random random = new Random();
            bool quit = false;

            while (!quit)
            {
                if (_scriptures.Count == 0)
                {
                    Console.WriteLine("All scriptures in the database have been viewed. Exiting the program.");
                    break; // Exit the main loop
                }

                var scripture = GetRandomScripture(random);
                _usedScriptures.Add(scripture);
                _scriptures.Remove(scripture);

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
                        scripture.HideRandomWord(random);
                        wordsLeft = scripture.Words.Count(w => !w.IsHidden);

                        Console.WriteLine("\nPress Enter to continue...");
                        Console.ReadLine();

                        if (wordsLeft == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\nCongratulations! All words are hidden!");
                            scriptureComplete = true;
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
                            break;
                        }
                        else if (response?.ToLower() == "no")
                        {
                            quit = true;
                            break;
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
            if (File.Exists(filePath))
            {
                try
                {
                    var lines = File.ReadAllLines(filePath);

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
