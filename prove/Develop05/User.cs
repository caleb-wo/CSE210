using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

[Serializable]
class User
{
    public static TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    [JsonInclude]
    private string _name;
    [JsonInclude]
    private int _score = 0;
    [JsonInclude]
    private List<Goal> _goals = new List<Goal>();
    [JsonInclude]
    private List<Goal> _completedGoals = new List<Goal>();
    [JsonInclude]
    private string _title;

    public User() { }

    public User(string name)
    {
        _name = User.textInfo.ToTitleCase(name);
    }

    // GETTERS
    public string getName() { return _name; }
    public int getScore() { return _score; }
    public List<Goal> getGoals() { return _goals; }
    public List<Goal> getCompletedGoals() { return _completedGoals; }
    public string getTitle() { return _title; }

    public void addGoal(Goal goal)
    {
        getGoals().Add(goal);
    }

    // SETTERS for deserialization
    public void setName(string name) { _name = name; }
    public void setScore(int score) { _score = score; }
    public void setGoals(List<Goal> goals) { _goals = goals; }
    public void setTitle(string title) { _title = title; }

    public void addPoints(int points)
    {
        _score += points;
    }

    public void Finish(Goal completedGoal)
    {
        _goals.Remove(completedGoal);
        _completedGoals.Add(completedGoal);
    }


    // Deserialization, returns list of users. 
    public static User loadUser(string filename)
    {
        try
        {
            if (File.Exists(filename))
            {
                string jsonString = File.ReadAllText(filename);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true,
                    IncludeFields = true
                };
                return JsonSerializer.Deserialize<User>(jsonString, options);
            }
            else
            {
                Console.WriteLine($"File not found: {filename}");
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    } 
    // Select User (initialize user)
    public static User initUser(User loadedUser, string name)
    {
        if (loadedUser == null)
        {
            System.Console.WriteLine("It looks like its your first time!");
            System.Console.WriteLine("We'll add you to keep track of your progess.");
            System.Console.WriteLine("What's your name: ");
            string userName = Console.ReadLine();
            User newUser = new User(userName);
            return newUser;
        }
        else
        {
            return loadedUser;
        }
    }

    public static void DetailUser(User user)
    {
        System.Console.WriteLine($"Name: {user.getName()}, {user.getTitle()}\nScore: {user.getScore()}\nComplete Goals -({user.getCompletedGoals().Count}][{user.getGoals().Count})- Remaining Goals\n");
    }
}