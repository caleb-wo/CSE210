using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        int userActivity = 0;
        while (userActivity != 4)
        {
            Console.WriteLine("\n \n");
            Console.WriteLine("|==========Rest Activities==========|");
            Console.WriteLine("   ( 1 ) Breathing  Activity");
            Console.WriteLine("   ( 2 ) Reflection Activity");
            Console.WriteLine("   ( 3 ) Listing    Activity");
            Console.WriteLine("   ( 4 ) QUIT");
            Console.WriteLine("|===================================|");
            Console.Write("Please enter a number: ");
            userActivity = int.Parse(Console.ReadLine());
            
            if (userActivity == 4) {Console.WriteLine(
                "Goodbye. Comeback if you need us!"
                );Environment.Exit(0);}
            
            int time = RestActivity.startingMessage();
            switch (userActivity)
            {
                case 1:
                    BreathingActivity bAct = new BreathingActivity(time);
                    bAct.doActivity(RestActivity.getDuration(bAct));
                    RestActivity.endMessage();
                    break;
                 case 2:
                     ReflectionActivity refAct = new ReflectionActivity(time);
                     refAct.doActivity(RestActivity.getDuration(refAct));
                     RestActivity.endMessage();
                     break;               
                 case 3:
                     ListingActivity listAct = new ListingActivity(time);
                     listAct.doActivity(RestActivity.getDuration(listAct));
                     RestActivity.endMessage();
                     break;                
                 default:
                    Console.WriteLine("Invalid input. Try again.");
                     break;
            }
        }
    }
}