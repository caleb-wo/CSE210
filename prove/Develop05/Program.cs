using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        string filename = "users.json";
        string goalDesc;
        bool isNew = false;
        int index;
        int userChoice;
        int subChoiceInt;
        string subChoiceStr;
        User currentUser = null;
        User loadedUser;
        
        System.Console.WriteLine("Before we start, is this your first time? (y/n)");
        subChoiceStr = Console.ReadLine().Trim().ToLower();
        if (subChoiceStr == "y") { isNew = true; }
        Console.WriteLine("|============Eternal Quest Game============|");
        while(!EternalQuestGame.isUserDone())
        {
            System.Console.WriteLine("                    —Menu—");
            System.Console.WriteLine("                (1) Load User");
            System.Console.WriteLine("                (2) Save User");
            System.Console.WriteLine("                (3) Display Goals");
            System.Console.WriteLine("                (4) Display Compeleted Goals");
            System.Console.WriteLine("                (5) New Goal");
            System.Console.WriteLine("                (6) Complete Goal");
            System.Console.WriteLine("                (7) Stat Report");
            System.Console.WriteLine("                (8) Quit");
            System.Console.WriteLine();
            System.Console.WriteLine("What would you like to do? ");
            userChoice = int.Parse(Console.ReadLine().Trim()); if (isNew) {userChoice = 1;}

            switch(userChoice)
            {
                case 1: // Load User
                    index = 1;
                    loadedUser = User.loadUser(filename);
                    if (loadedUser == null)
                    {
                        isNew = false;
                        System.Console.WriteLine("Enter name of user: ");
                        subChoiceStr = User.textInfo.ToTitleCase(Console.ReadLine().Trim());
                        currentUser = User.initUser( loadedUser, User.textInfo.ToTitleCase(subChoiceStr) );
                        EternalQuestGame.SetUser(currentUser);
                        System.Console.WriteLine("Thank you! Lets add your first goal.");
                        System.Console.WriteLine("Is the goal small (1), medium (2), or large (3)? ");
                        subChoiceStr = Console.ReadLine().Trim();
                        System.Console.WriteLine("What is the description? ");
                        goalDesc = Console.ReadLine().Trim();
                        switch(subChoiceStr)
                        {
                            case "1":
                                SmallGoal newSmallGoal = new SmallGoal( EternalQuestGame.getUser(), goalDesc );
                                EternalQuestGame.getUser().addGoal(newSmallGoal);
                                break;
                            case "2":
                                MediumGoal newMediumGoal = new MediumGoal( EternalQuestGame.getUser(), goalDesc );
                                EternalQuestGame.getUser().addGoal(newMediumGoal);
                                break;
                            case "3":
                                LargeGoal newLargeGoal = new LargeGoal( EternalQuestGame.getUser(), goalDesc );
                                EternalQuestGame.getUser().addGoal(newLargeGoal);
                                break;
                            default:
                                System.Console.WriteLine("Invalid input. Try again!");
                                break;
                        }
                    }
                    else 
                    {
                        System.Console.WriteLine("The saved user is "+loadedUser.getName());
                        System.Console.WriteLine("Loading...");
                        System.Console.WriteLine("User ready. Click [Enter].");
                        subChoiceStr = User.textInfo.ToTitleCase(Console.ReadLine().Trim());
                        currentUser = User.initUser( loadedUser, User.textInfo.ToTitleCase(subChoiceStr) );
                        EternalQuestGame.SetUser(currentUser);
                    }
                    break;
                case 2: // Save User
                    if(currentUser != null)
                    {
                        EternalQuestGame.saveGameUser(filename);
                    } else 
                    {
                        System.Console.WriteLine("User not loaded yet!");
                    }
                    break;
                case 3: // Display Goals
                    EternalQuestGame.DisplayGoals(false);
                    break;
                case 4: // Displat Completed Goals
                    EternalQuestGame.DisplayGoals(true);
                    break;
                case 5: // New Goal
                    System.Console.WriteLine("Is the goal small (1), medium (2), or large (3)? ");
                    subChoiceStr = Console.ReadLine().Trim();
                    System.Console.WriteLine("What is the description? ");
                    goalDesc = Console.ReadLine().Trim();
                    switch(subChoiceStr)
                    {
                        case "1":
                            SmallGoal newSmallGoal = new SmallGoal( EternalQuestGame.getUser(), goalDesc );
                            EternalQuestGame.getUser().addGoal(newSmallGoal);
                            break;
                        case "2":
                            MediumGoal newMediumGoal = new MediumGoal( EternalQuestGame.getUser(), goalDesc );
                            EternalQuestGame.getUser().addGoal(newMediumGoal);
                            break;
                        case "3":
                            LargeGoal newLargeGoal = new LargeGoal( EternalQuestGame.getUser(), goalDesc );
                            EternalQuestGame.getUser().addGoal(newLargeGoal);
                            break;
                        default:
                            System.Console.WriteLine("Invalid input. Try again!");
                            break;
                    }
                    break;
                case 6: // Complete Goal
                    index = 1;
                    System.Console.WriteLine("Which goal do you want to complete?");
                    foreach (Goal goal in EternalQuestGame.getUser().getGoals())
                    {
                        System.Console.WriteLine($"{index}. "+Goal.Stringify(goal));
                        index++;
                    } 
                    System.Console.WriteLine("Goal to complete: ");
                    subChoiceInt = int.Parse(Console.ReadLine().Trim());
                    EternalQuestGame.getUser().getGoals()[subChoiceInt - 1].Complete();
                    EternalQuestGame.BestowHonors();
                    break;
                case 7: // Stat Report
                    System.Console.WriteLine("Current user report: ");
                    User.DetailUser(EternalQuestGame.getUser());
                    break;
                case 8: // Quit
                    System.Console.WriteLine("Goodbye, great job today!");
                    EternalQuestGame.isFinished();
                    break;
                default:
                    System.Console.WriteLine("Invalid input. Try again.");
                    break;
            }

        }
        Console.WriteLine("|============See you soon!=================|");
    }
}