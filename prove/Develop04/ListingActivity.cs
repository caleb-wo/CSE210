
using System.Diagnostics;

public class ListingActivity : RestActivity
{
    Random random = new Random();
    List<Prompt> questions = new List<Prompt>
    {
         new Prompt("Who are people that you appreciate?")
        ,new Prompt("What are personal strengths of yours?")
        ,new Prompt("Who are people that you have helped this week?")
        ,new Prompt("When have you felt the Holy Ghost this month?")
        ,new Prompt("Who are some of your personal heroes?")
    };
    List<string> answers = new List<string>();
    
    public ListingActivity(int duration) : 
        base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area."
            , duration) {}

    public string getQuestion()
    {
        return questions[random.Next(questions.Count)].ToString();
    }

    public void doActivity(int time)
    {
        timer.Restart();
        string response;
        Console.WriteLine("==========WELCOME TO YOUR LISTING ACTIVITY==========");
        RestActivity.timer.Start();
        Console.WriteLine("Please think deeply on the following prompt:");
        Console.WriteLine(getQuestion());
        Console.WriteLine("Please take time to reflect...\n");
        for (int i = 5; i > 0; i--)
        {
            Console.Write($"\r{i}");
            Thread.Sleep(1000); // Wait for 1 second
        }
        Console.WriteLine("\nNow please write down as much things as you can think of. Press [ENTER] to add another item.");
        do
        {
            response = Console.ReadLine();
            answers.Add(response);
        } while (RestActivity.timer.Elapsed.TotalSeconds < time);
        RestActivity.timer.Stop();
        Console.WriteLine($"You thought of {answers.Count} things. Amazing!");
        Console.WriteLine("Here they are:");
        Console.WriteLine("==========");
        answers.ForEach(Console.WriteLine);
        Console.WriteLine("==========");
        Console.WriteLine("Great job!");                 
    }
}