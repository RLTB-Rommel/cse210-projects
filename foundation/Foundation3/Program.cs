using System;
using System.Collections.Generic;

//base class for activities
public abstract class Activity
{
    private DateTime _date; // private member variable for date
    private int _duration; //duration in minutes, private member variable for duration

    public Activity(DateTime date, int duration)
    {
        _date = date;
        _duration = duration; // property to access duration
    }

    //public property to access the duration

    protected int Duration => _duration; //making the duration accessible to the derived classes
    public abstract double GetDistance(); //abstract method for distance
    public abstract double GetSpeed(); //abstract method for speed
    public abstract double GetPace(); //abstract method for pace

    public string GetSummary()
    {
        double distanceKm = GetDistance()*1.60934; //convert miles to kilometer
        double speedKph = GetSpeed()*1.60934; // convert mph to kph
        
        
        return $"{_date:dd MMM yyyy} {GetType().Name} ({_duration} min) - " +
           $"Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile\n" +
           $"{_date:dd MMM yyyy} {GetType().Name} ({_duration} min) - " +
           $"Distance: {distanceKm:F1} km, Speed: {speedKph:F1} kph, Pace: {60 / speedKph:F2} min per km";
    }

}

    // Derived class for running

public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int duration, double distance) : base (date, duration)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance; //returns the distance for running
    public override double GetSpeed() => (_distance/Duration) * 60; // speed in miles per hour
    public override double GetPace() => _distance == 0? double.PositiveInfinity : Duration / _distance; // pace in min/mile
       
}

//derived  class for cycling

public class Cycling : Activity
{
    private double _speed; // in mph
    public Cycling(DateTime date, int duration, double speed): base(date, duration)
    {
        _speed = speed;
    }
    public override double GetDistance() => (_speed/60)*Duration; //distance base on speed
    public override double GetSpeed() => _speed; // returns the speed
    public override double GetPace() => 60 / _speed; // pace in minutes/mile
      
}

//Derived class for swimming

public class Swimming: Activity
{
    private int _laps;
    public Swimming(DateTime date, int duration, int laps): base(date, duration)
    {
        _laps = laps;
    }
    

    public override double  GetDistance() => _laps * 50/1609.34; // in miles
    public override double GetSpeed() => (GetDistance()/Duration) * 60; // in kilometer per hour
    public override double GetPace() => Duration / GetDistance() == 0 ? double.PositiveInfinity :  Duration/GetDistance(); // pace in minutes per kilometer

}

// Main program

public class Program
{
    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running( new DateTime(2024, 11, 3), 30, 3.0),
            new Cycling( new DateTime(2024, 11, 4),45,12.0),
            new Swimming(new DateTime(2024, 11, 5), 30, 20 )
        };
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}