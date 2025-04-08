using System;
using System.Threading;


class Program
{
    static void Main(string[] args)
    {
        bool isActive = true;
        System.Console.WriteLine("|=======Welcome to your Fitness Goal Tracker=======|");
        System.Console.WriteLine("   < A > New User");
        System.Console.WriteLine("   < B > Returning User");
        string choice = Console.ReadLine().Trim().ToUpper();

        bool firstTime = false;
        User user = null;
        switch (choice)
        {
            case "A":
                user = User.InitNewUser();
                firstTime = true;
                
            break;
            case "B":
                System.Console.WriteLine("Would you like to load your file by name or userId? (answer with \"n\" or \"id\")");
                string loadPreference = Console.ReadLine().Trim().ToUpper();
                if(loadPreference == "N")
                {
                    System.Console.WriteLine("What is the name on file? ");
                    string searchName = Console.ReadLine().Trim();
                    user = User.LoadUserByName(searchName);
                } else if (loadPreference == "ID")
                {
                    System.Console.WriteLine("What is the ID? ");
                    string searchId = Console.ReadLine().Trim();
                    user = User.LoadUser(searchId);
                } else 
                {
                    System.Console.WriteLine("ERROR. Invalid input.");
                }
            break;
            default:
                    System.Console.WriteLine("ERROR. Invalid input.");
            break;
        }
    
        string menuChoice;
        while(isActive)
        {
            if (firstTime)
            {
                DisplayFirstTimeMenu();
                firstTime = false;
            } else 
            {
                DisplayMainMenu();
            }
            menuChoice = Console.ReadLine().Trim().ToUpper();
            switch (menuChoice)
            {
                case "A": // See all goals
                    user.GetGoals().ForEach( goal => goal.PrintGoal() );
                    Thread.Sleep(5000);
                    break;
                case "B": // See active goals
                    user.GetActiveGoals().ForEach( goal => goal.PrintGoal() );
                    Thread.Sleep(5000);
                    break;
                case "C": // See Completed goals
                    user.GetInactiveGoals().ForEach( goal => goal.PrintGoal() );
                    Thread.Sleep(5000);
                    break;
                case "D": // New goal
                    user.NewGoal();
                    Thread.Sleep(5000);
                    break;
                case "E": // Goal check in 
                    user.GoalCheckIn();
                    Thread.Sleep(5000);
                    break;
                case "F": // See Philosophy
                    user.GetPhilosophy().PrintPhilosophy();
                    Thread.Sleep(5000);
                    break;
                case "G": // Complete Philosophy
                    user.CompletePhilosophy();
                    Thread.Sleep(5000);
                    break;
                case "H": // BMI
                    FitnessMetrics.CalcBMI( user );
                    Thread.Sleep(5000);
                    break;
                case "I": // BMR
                    FitnessMetrics.CalcBMR( user );
                    Thread.Sleep(5000);
                    break;
                case "J": // TDEE
                    FitnessMetrics.CalcTDEE( user );
                    Thread.Sleep(5000);
                    break;
                case "K": // One-rep-max
                    FitnessMetrics.CalcOneRepMax();
                    Thread.Sleep(5000);
                    break;
                case "L": // Max BPM
                    FitnessMetrics.CalcMaxBPM( user );
                    Thread.Sleep(5000);
                    break;
                case "M": // Calc All
                    System.Console.WriteLine("For one-rep-max what was the weight you lifted?");
                    double w = double.Parse(Console.ReadLine().Trim());
                    System.Console.WriteLine("How many reps did you reach?");
                    double r = double.Parse(Console.ReadLine().Trim());
                    FitnessMetrics.SuperReport( user, w, r );
                    Thread.Sleep(5000);
                    break;
                case "N": // Print User
                    user.PrintUser();
                    Thread.Sleep(10000);
                    break;
                case "X": // Quit
                    System.Console.WriteLine("See you soon! Thanks for checking in.");
                    user.SaveUserToCsv();
                    isActive = false;
                    Thread.Sleep(5000);
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please choose a letter from A to M or X.");
                    break;
            }

        }
    }

    static void DisplayMainMenu()
    {
        Console.Clear();
        System.Console.WriteLine("|============================Main Menu============================|");
        System.Console.WriteLine("   < A > See All Goals");
        System.Console.WriteLine("   < B > See Active Goals");
        System.Console.WriteLine("   < C > See Completed Goals");
        System.Console.WriteLine("   < D > New Goal");
        System.Console.WriteLine("   < E > Check In");
        System.Console.WriteLine("   < F > See Philosophy");
        System.Console.WriteLine("   < G > Complete Philosophy");
        System.Console.WriteLine("   < H > Calculate BMI");
        System.Console.WriteLine("   < I > Calculate BMR");
        System.Console.WriteLine("   < J > Calculate TDEE");
        System.Console.WriteLine("   < K > Calculate One-rep-max");
        System.Console.WriteLine("   < L > Calculate Max BPM");
        System.Console.WriteLine("   < M > Calculate All");
        System.Console.WriteLine("   < X > Quit");
    }

    static void DisplayFirstTimeMenu()
    {
        Console.Clear();
        System.Console.WriteLine("|============================Limited Menu============================|");
        System.Console.WriteLine("   < D > New Goal");
        System.Console.WriteLine("   < H > Calculate BMI");
        System.Console.WriteLine("   < I > Calculate BMR");
        System.Console.WriteLine("   < J > Calculate TDEE");
        System.Console.WriteLine("   < K > Calculate One-rep-max");
        System.Console.WriteLine("   < L > Calculate Max BPM");
        System.Console.WriteLine("   < M > Calculate All");
    }
}