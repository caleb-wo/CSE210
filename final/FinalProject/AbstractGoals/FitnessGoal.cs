using System.ComponentModel.Design;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Linq;
using Goals;

abstract class FitnessGoal : Goal
{
    // Constuctor
    public FitnessGoal
    (   
        string OwnerId,
        string Description, string Reward
    ) : base( OwnerId, Description, Reward ){} 

    // CSV Constructor
    public FitnessGoal
    (   
        string OwnerId,
        string Description, string Reward,
        bool IsComplete, string StartDate,
        string EndDate
    ) : base( OwnerId, Description, Reward, IsComplete, StartDate, EndDate ){} 
    // Abstract Methods
    public abstract void CheckIn();

    // Static Methods
    public static List<int> StringsToInt( List<string> list )
    {
        if (list.Count > 0)
        {
            return list.Select(str => int.Parse(str))
                    .ToList();
        } else
        {
            return new List<int>();
        }
    }

    public static List<double> StringsToDouble( List<string> list )
    {
        if (list.Count > 0)
        {
            return list.Select(str => double.Parse(str))
                    .ToList();
        } else
        {
            return new List<double>();
        }
    }

    public static int GetDifference( List<int> numbers )
    {
        return numbers[0] - numbers[1];
    }

    public static double GetDifference( List<double> numbers )
    {
        return numbers[0] - numbers[1];
    }

}