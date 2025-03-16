using System.Data.Common;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

class EternalQuestGame
{
    static private User _user;
    static bool isDone = false;
    static private readonly Dictionary<string, int> TITLES_OF_HONOR = new Dictionary<string, int>{
        { "Novice", 1_000 }
        ,{ "Go-Getter", 10_000 }
        ,{ "Goalguppie", 20_000 }
        ,{ "Perfectionist", 30_000 }
        ,{ "Goalgrammer", 40_000}
        ,{ "Master-Acheiver", 50_000}
        ,{ "Goal Conqueror", 60_000 }
        ,{ "The King of Goals", 70_000 }
        ,{ "Galactic Goalian", 80_000 }
        ,{ "Intergalactic Super Goalguppie", 90_000 }
        ,{ "Multiversal Goal Conqueror", 100_000 }
    };

    public static void isFinished()
    {
        EternalQuestGame.isDone = true;
    }

    // Serialize user into JSON
    public static void saveGameUser(string filename) 
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
                IncludeFields = true
            };
            string jsonString = JsonSerializer.Serialize(_user, options);
            File.WriteAllText(filename, jsonString);
            System.Console.WriteLine("Save successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving user: {ex.Message}");
        }
    }

    public static bool isUserDone()
    {
        return EternalQuestGame.isDone;
    }

    public static void ListGoals()
    {
        List<Goal> goals = getUser().getGoals();
        int index = 1;
        foreach (Goal goal in goals)
        {
            Console.WriteLine($"{index}. {goal.getDescription()}");
            index++;
        }
    }

    public static void ListCompletedGoals()
    {
        List<Goal> completeGoals = getUser().getCompletedGoals();
        int index = 1;
        foreach (Goal goal in completeGoals)
        {
            Console.WriteLine($"{index}. {goal.getDescription()}");
            index++;
        }
    }

    public static void SetUser(User u)
    {
        EternalQuestGame._user = u;
    }
    public static User getUser()
    {
        return EternalQuestGame._user;
    }

    public static void BestowHonors()
    {
        KeyValuePair<string, int> awardedHonor = new KeyValuePair<string, int>("ERROR", 000);
        int score = getUser().getScore();
        foreach(KeyValuePair<string, int> honor in EternalQuestGame.TITLES_OF_HONOR)
        {
            if (honor.Value < score) { getUser().setTitle(honor.Key); awardedHonor = honor; }
        } 
        System.Console.WriteLine($"Congratulations! You've earned a new title of honor. It's titled: \"{awardedHonor.Key}\"!\nIt's worth {awardedHonor.Value} points.");
    }

    public static void DisplayGoals( bool showCompleted )
    {
        List<Goal> goalContainer = new List<Goal>();
        if (showCompleted)
        {
            goalContainer = getUser().getCompletedGoals();
        } else 
        {
            goalContainer = getUser().getGoals();
        }

        foreach(Goal goal in goalContainer)
        {
            System.Console.WriteLine(Goal.Stringify(goal));
        }
    }
}