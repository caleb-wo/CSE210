using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

public abstract class RestActivity
{
    private string _name;
    private int _duration;
    private readonly string _description;

    private static readonly string _endDescription =
        "We are so happy we could be there for you today. If you need to rest again try another activity.\nOtherwise, click '4' and please come back!";
    public static readonly Stopwatch timer = new Stopwatch();
    private static Stopwatch _freezeTimer = new Stopwatch();

    public RestActivity(string name, string description, int duration)
    {
        _name = name;
        _description = description;
        _duration = duration;
    }

    //STATIC methods    
    public static string getDescription(RestActivity a)
    {
        return a._description;
    }
    public static int getDuration(RestActivity a)
        {
            return a._duration;
        }
    public static int startingMessage()
    {
        Console.WriteLine("How long would you like to be helped today?");
        string getInput = Console.ReadLine();
        int time = int.Parse(getInput);
        Console.WriteLine("Okay.");

        return time;
    }

    public static string endMessage()
    {
        return _endDescription;
    }

    public static void freeze(int seconds)
    {
        char[] animation = { '|', '/', '—', '\\' };
        int animationGuide = 0;
        _freezeTimer.Start();
        while (_freezeTimer.Elapsed.TotalSeconds < seconds)
        {
            Console.Write($"\r{animation[animationGuide % animation.Length]} ");
            animationGuide++;
            Thread.Sleep(250); 
        }
        Console.WriteLine("\r \r");
        _freezeTimer.Stop(); _freezeTimer.Reset();
    } 
}