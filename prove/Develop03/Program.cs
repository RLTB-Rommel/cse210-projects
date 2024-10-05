using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorization
{
    //class represent a scripture reference
    public class ScriptureReference
    {
        public string Reference {get;private set;}
        public ScriptureReference(string reference)
        {
            Reference = reference;
        }
        public ScriptureReference(string book, int chapter, int verse)
            {
                Reference = $"{book}{chapter}:{verse}";
            }
        public ScriptureReference (string book, int chapter, int startverse, int endverse)
            {
                Reference = $"{book}{chapter}:{startverse}-{endverse}";
            }
    }   

    //class represent a word in the scripture

    public class word
    {
        
        public string Text{get; private set;}
        public bool IsHidden{get; set;}
        public word (string text)
        {
            Text = text;
            IsHidden = false;
        }

        public override string ToString()
        {
            return IsHidden ? "_____" :Text;//display hidden word as "_______"
        }
        
    }

    //class to represent a scripture

    public class Scripture
    {
        public ScriptureReference Reference {get; private set;}
        public List<word> Words{get; private set;}

        public Scripture (ScriptureReference reference, string text)
        {
            Reference = reference;
            Words = text.Split (' ').Select (w=> new word(w)).ToList();
        }

        public void HideRandomWord (Random random)
        {
            
            //check if all words are already hidden

            if (Words.All(w=>w.IsHidden))
                return; //exit if all words are hidden
            int index;
            do
            {       
                index = random.Next(Words.Count);

            }
            while (Words[index].IsHidden); // ensure hiding only visible words
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

    //Main program class

    class Program
    {
        private static List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new ScriptureReference("Romans 8:38-39"),
            "For I am convinced that neither death nor life, neither angels nor demons, " +
            "neither the present nor the future, nor any powers, neither height nor depth, " +
            "nor anything else in all creation, will be able to separate us from the love of God that is in Christ Jesus our Lord."),
            
            new Scripture(new ScriptureReference("Luke 24:38-39"),
            "And He said unto them, why are ye troubled? and why do thoughts arise in your hearts? " +
            "Behold my hands and my feet, that it is I myself: handle me, and see; for a spirit hath not flesh and bones, as ye see me have."),
                        
            new Scripture(new ScriptureReference("John 10:16"),
            "And other sheep I have, which are not of this fold: them also I must bring, " +
            "and they shall hear my voice; and there shall be one fold, and one shepherd."),
            
            new Scripture(new ScriptureReference("Matthew 22:37-40"),
            "Jesus replied: “ ‘Love the Lord your God with all your heart and with all your soul and with all your mind.’ " +
            "This is the first and greatest commandment. And the second is like it: ‘Love your neighbor as yourself.’ " +
            "All the Law and the Prophets hang on these two commandments.”"),
            
            new Scripture(new ScriptureReference("John 16:33"),
            "I have told you all this so that you may have peace in me. Here on earth you will have many trials and sorrows. " +
            "But take heart, because I have overcome the world.")
            
        };

        static void Main(string[] args)
        {
            Random random = new Random();
            bool quit = false;

            while (!quit)
            {
                Console.Clear();
                var scripture = GetRandomScripture(random);
                bool scriptureComplete = false;

                while(!scriptureComplete)

                {
                    Console.Clear();
                    Console.WriteLine(scripture); //display the current scripture

                        Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
                        string input = Console.ReadLine();
                        if (input?.ToLower()=="quit")

                        {
                            quit = true; // exit if user types quit
                            break;
                        }

                        else
                        {
                            scripture.HideRandomWord(random); // attempt to hide a random word
                            if(scripture.AllWordsHidden())  // check if all words are now hidden
                            {
                                scriptureComplete = true; //mark scripture as complete
                                Console.WriteLine("\n Congratulations.");
                                Console.WriteLine("\n Press enter to continue or type 'quit' to exit.");
                                input = Console.ReadLine();
                                if(input?.ToLower() == "quit") 

                            {
                                quit = true; // exit if user chooses to quit
                            }
                        }
                    }      
                }
            }
        }
        private static Scripture GetRandomScripture(Random random)
        {
            //Random random = new Random();
            return scriptures[random.Next(scriptures.Count)];
        }
    }
}

