using System.ComponentModel.Design;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Linq;

abstract class FitnessGoal : Goal
{
    // Fields
    private string _UserWeight;
    private string _UserHeight;
    private int _UserAge;
    private bool _IsMale;

    // Constuctor
    public FitnessGoal
    (   
        int OwnerId,
        string Description, string Reward,
        string Weight, string Height, 
        int Age, bool IsMale
    ) : base( OwnerId, Description, Reward )
    {
        _UserWeight = Weight;
        _UserHeight = Height;
        _UserAge = Age;
        _IsMale = IsMale;
    } 

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