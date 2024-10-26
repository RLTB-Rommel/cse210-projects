using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


[Serializable]
public abstract class Goal
{
    public string Name {get; private set;}
    protected int _points;
    protected int _achievements;

    protected Goal(string name)
    {
        Name = name;
        _achievements = 0;
    }
    public abstract void RecordAchievement(GoalTracker tracker);
    public abstract bool IsComplete();
    public abstract string GetGoalStatus();
    public int GetPoints() => _points;
}
[Serializable]
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points):base(name)
    {
        _points = points;
    }

    public override void RecordAchievement(GoalTracker tracker)
    {
        _achievements++;
        tracker.AddScore(_points);       
        Console.WriteLine($"You've completed: {Name}");

    }
    public override bool IsComplete() =>_achievements > 0;
    public override string GetGoalStatus() => IsComplete() ? "[X]" + Name : "[ ]" + Name;

}

[Serializable]
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points):base(name)
    {
        _points = points;
    }

    public override void RecordAchievement(GoalTracker tracker)
    {
        _achievements++;
        tracker.AddScore(_points);
        Console.WriteLine($"You've gained {_points} points for {Name}. Total: {_achievements * _points}");
    }
    public override bool IsComplete() =>false;
    public override string GetGoalStatus() => "[ ]" + Name + $"(Total Points: {_achievements * _points})";

}
[Serializable]
public class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    public ChecklistGoal(string name, int points, int target, int bonus):base(name)
    {
        _points = points;
        _target = target;
        _bonus = bonus;
    }

    public override void RecordAchievement(GoalTracker tracker)
    {
       if(_achievements <_target)
       {
            _achievements++;
            tracker.AddScore(_points);
            Console.WriteLine($"You've gained {_points} points for {Name}. Progress: {_achievements}/{_target}");
       
            if (IsComplete())
            {
                Console.WriteLine($"Bonus!You've completed {Name}! Bonus Points: {_bonus}");
                tracker.AddScore(_bonus);
            }
     
        }
        else
        {
            Console.WriteLine($"Goal {Name} already completed!");
        }
    }
    public override bool IsComplete() =>_achievements >= _target;
    public override string GetGoalStatus() => $"{(IsComplete() ? "[X]" : "[ ]")} {Name} (Completed: {_achievements}/{_target})";

}
[Serializable]
public class GoalTracker
{
    private List<Goal> _goals = new List<Goal>();
    public int TotalScore {get; private set;}
    
    public List<Goal> Goals => _goals;
    
    public void AddGoal (Goal goal)
    {
        _goals.Add(goal);
    }
        public void AddScore (int points)
    {
        TotalScore += points;
    }
    public void RecordAchievement(string goalName)
    {
        var goal = _goals.Find(g=>g.Name == goalName);
        if (goal !=null)
        {
            goal.RecordAchievement(this);
            TotalScore += goal.GetPoints();
        }
    }

    public void ShowGoals()
    {
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetGoalStatus());
        }
        Console.WriteLine($"\nTotal Score:{TotalScore}");
    }

    public void Save(string filePath)
    {
        try //creativity feature # 1 error handling  in saving the file
        {
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }
    public static GoalTracker Load(string filePath)
       
    {
        try
        {
        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<GoalTracker>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
            return new GoalTracker(); //return a new tracker  if loading fails
        }
    }
}   
class Program
{
    static void Main(string []args)
    {
        GoalTracker tracker = new GoalTracker();
    
        bool running = true;
        while (running)
        {
            Console.WriteLine($"\nYou have  {tracker.TotalScore} points.\n");
            Console.WriteLine("Menu Options : ");
            Console.WriteLine("\n1. Create New Goals");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit\n");            
            Console.Write("Select a choice from the menu: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateNewGoals(tracker);
                    break;
                case "2":
                    tracker.ShowGoals();
                    break;
                case "3":
                    Console.WriteLine("What is the filename for the goal file? (e.g. exercise.txt) ");
                    string saveFile = Console.ReadLine();
                    tracker.Save(saveFile);
                    Console.WriteLine("Goals saved!");
                    break;
                case "4":
                    Console.WriteLine("What is the filename for the goal file? (e.g. exercise.txt)");
                    string loadFile = Console.ReadLine();
                    tracker = GoalTracker.Load(loadFile);
                    Console.WriteLine("Goals loaded!");
                    break;
                case "5":
                    RecordEvent(tracker);
                    break;
                case "6":
                    running=false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
    static void CreateNewGoals(GoalTracker tracker)
    {
        Console.WriteLine("\nThe types of Goals are:\n");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal:");
        Console.WriteLine("3. CheckList Goas:");


        string type;
        while (true)
        {
            Console.Write("\nWhich type of Goal would you like to create? ");
            type = Console.ReadLine();
        
            if (type == "1" || type == "2" || type == "3")
            {
                break; //valid input, will exit the loop
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1, 2, 3.");
            }
        }
            
        Console.Write("\nWhat is the name of your goal? ");
        var name = Console.ReadLine();
        Console.Write("Give a short description of it: ");
        var description = Console.ReadLine();

        int points;
        while (true)
        {

            Console.Write("What is the amount of points associated with this goal? ");
            if(int.TryParse(Console.ReadLine(), out points))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for points."); // creativity feature #2 shows this message if encoded a value other than a number or integer.
            }
        }
    
        int target = 0;
        int bonus = 0;

        if (type == "3")

            while (true)
            {
                Console.Write("How many times does this goal need to be accomplished for a bonus?");
                if(int.TryParse(Console.ReadLine(), out target))
                {
                    break; // valid integer, exits the loop
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for target.");
                }
            }
            while (true)

            {
                Console.Write("How much is the bonus for accomplishing that many times? ");
                if(int.TryParse(Console.ReadLine(), out target))                                
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for target.");
                }
            } 
        Goal newGoal;
        switch (type)
        {
            case "1":
                newGoal = new SimpleGoal(name,points);  
                break;
            
            case "2":
                newGoal = new EternalGoal(name,points);  
                break;
            case "3":
                newGoal = new ChecklistGoal(name,points,target,bonus);  
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }  
        tracker.AddGoal(newGoal);
        Console.WriteLine("Goal created successfully!");     
    }

    static void RecordEvent(GoalTracker tracker)
    {
        tracker.ShowGoals();
        Console.Write("Which goal did you accomplish: ");
        if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber > 0 && goalNumber <= tracker.Goals.Count)
        {
            tracker.RecordAchievement(tracker.Goals[goalNumber - 1].Name);
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }
}