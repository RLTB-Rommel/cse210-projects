using System;
using System.Collections.Generic;

class Comment

{
    public string CommenterName {get; private set;}
    public string Text{get; private set;} 
    public Comment(string commenterName, string text)
    {
       CommenterName = commenterName; 
       Text = text;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {Text}";
    }
}
class Video
{
    public string Title {get; private set;}
    public string Author {get; private set;}
    public int Length {get; private set;}
    private List<Comment> comments;

    public Video(string title, string author, int length)

    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int NumberOfComments()
    {
        return comments.Count;
    }

    public override string ToString()
    {
        string commentsStr = string.Join(Environment.NewLine,comments);
        return $"Title:{Title}\n" +
               $"Author:{Author}\n" +
               $"Length:{Length} seconds\n" +
               $"Number of Comments:{NumberOfComments()}\n" +
               $"Comments:\n{commentsStr}\n";

    }

}

class Program
{
    static void Main(string[]args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Understanding C#","Alice", 300);
        video1.AddComment(new Comment("Bob","Great explanations!"));
        video1.AddComment(new Comment("Benson","Very clear tutorial."));
        video1.AddComment(new Comment("Ezra","Helped me a lot, thanks!"));

        Video video2 = new Video("Introduction to Java","Bob", 240);
        video2.AddComment(new Comment("Spencer","I needed that!"));
        video2.AddComment(new Comment("Thomas","Very helpful."));
        video2.AddComment(new Comment("Henry","I'll practice using Java!"));

        Video video3 = new Video("Advanced Python","Charlie", 400);
        video3.AddComment(new Comment("Boyd","Great find"));
        video3.AddComment(new Comment("Diether","So simple."));
        video3.AddComment(new Comment("Russell","Thanks for the help!"));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        foreach (Video video in videos)
        {
            Console.WriteLine(video);
        }
    }
}