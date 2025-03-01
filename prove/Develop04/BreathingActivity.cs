class BreathingActivity : RestActivity
{
    private readonly string INHALE = "Breath in...";
    private readonly string EXHALE = "Breath out...";

    public BreathingActivity(int duration)
        : base("Breathing Activity", 
            "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.",
            duration) {}

    public void doInhale(int count)
    {
        Console.WriteLine(INHALE);
        RestActivity.freeze(count);
    }
    public void doExhale(int count)
    {
        Console.WriteLine(EXHALE);
        RestActivity.freeze(count);
    }

    public void doActivity(int duration)
    {
        timer.Restart();
        Console.WriteLine("==========WELCOME TO YOUR BREATHING ACTIVITY==========");
        RestActivity.timer.Start(); 

        while (RestActivity.timer.Elapsed.TotalSeconds < duration)
        {
            if (RestActivity.timer.Elapsed.TotalSeconds + 18 > duration) 
                break;

            doInhale(10);
            doExhale(8);
        }

        Console.WriteLine("Great job!");
    }
}