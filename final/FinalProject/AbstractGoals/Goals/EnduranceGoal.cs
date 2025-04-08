using Tracker;

class EnduranceGoal : FitnessGoal
{
    // Fields
    private string _Exercise;
    private TimeLog _CurrentPR;
    private TimeLog _Target;
    private List<TimeLog> _History;

    // Constructor
    public EnduranceGoal( string OwnerId, string Description, 
                            string Reward, string Lift, 
                            string InitialPR, string TargetPR
    ) : base( OwnerId, Description, Reward )
    {
        _Exercise = Lift;
        _CurrentPR = new TimeLog( InitialPR );
        _Target = new TimeLog( TargetPR );

        _History = new List<TimeLog>();
        _History.Add(_CurrentPR);
    }

    // CSV Constructor
    public EnduranceGoal( string OwnerId, string Description, 
                            string Reward, bool IsComplete,
                            string DateStarted, string DateEnded,
                            string Lift, string InitialPR, string InitialPRDate,
                            string TargetPR, List<TimeLog> History
    ) : base( OwnerId, Description, Reward, IsComplete, DateStarted, DateEnded )
    {
        _Exercise = Lift;
        _CurrentPR = new TimeLog( InitialPR, InitialPRDate );
        _Target = new TimeLog( TargetPR );

        _History = History;
    }

    // Save Goal
    public string ToCsv()
    {
        string HistoryCsv = "";

        if (GetHistory().Count > 0)
        {
            GetHistory().ForEach( log =>{
                HistoryCsv += $"<><>{log.GetTime()}<><>{log.GetDate()}";
            });
        }
        

        return $"{GetCsvStarter()}<><>{GetExercise()}<><>{GetCurrentPr().GetTime()}<><>{GetCurrentPr().GetDate()}<><>{GetTargetPr().GetTime()}<><>HISTORY_START{HistoryCsv}";
    }
    
    public void SaveGoalToCsv()
    {
        // Define the directory and file path
        string csvDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv");
        string path = Path.Combine(csvDirectory, "goals.csv");
        
        // Create the directory if it doesn't exist
        if (!Directory.Exists(csvDirectory))
        {
            Directory.CreateDirectory(csvDirectory);
        }
        
        List<string> lines = new List<string>();
        if (File.Exists(path))
        {
            lines = File.ReadAllLines(path).ToList();
        }
        else
        {
            // Add header
            lines.Add("OwnerId<><>GoalType<><>GoalDescription<><>Reward<><>IsComplete<><>StartDate<><>EndDate<><>Exercise<><>CurrentTime<><>DateForCurrentTime<><>TargetTime<><>HISTORY_START<><>logs");
        }
        
        // Try to match by OwnerId and Description (assuming it's unique for that user)
        bool updated = false;
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
            if (lines[i].Contains($"<><>{GetOwnerId()}<><>") && lines[i].Contains($"<><>{GetDescription()}<><>"))
            {
                lines[i] = ToCsv(); // Replace with new goal data
                updated = true;
                break;
            }
        }
        
        if (!updated)
        {
            lines.Add(ToCsv());
        }
        
        File.WriteAllLines(path, lines);
    }

    // Load Goal Static
    public static EnduranceGoal FromCsv(string csvLine)
    {
        string[] parts = csvLine.Split(new string[] { "<><>" }, StringSplitOptions.None);
        
        // Find where history starts
        int historyStartIndex = Array.IndexOf(parts, "HISTORY_START");
        
        // Parse basic fields
        string ownerId = parts[0];
        string description = parts[2]; // Skipping GetType() or parts[1]
        string reward = parts[3];
        bool isComplete = bool.Parse(parts[4]);
        string startDate = parts[5];
        string endDate = parts[6];
        string exercise = parts[7];
        string currentTime = parts[8];
        string currentDate = parts[9];
        string targetTime = parts[10];
        
        // Parse history logs
        List<TimeLog> history = new List<TimeLog>();
        for (int i = historyStartIndex + 1; i < parts.Length - 1; i += 2)
        {
            string time = parts[i];
            string date = parts[i + 1];
            history.Add(new TimeLog(time, date));
        }
        
        // Create instance
        EnduranceGoal goal = new EnduranceGoal(
            ownerId,
            description,
            reward,
            isComplete,
            startDate,
            endDate,
            exercise,
            currentTime,
            currentDate,
            targetTime,
            history
        );
        
        return goal;
    }

    // Getters
    public string GetExercise(){ return _Exercise; }
    public TimeLog GetCurrentPr(){ return _CurrentPR; }
    public TimeLog GetTargetPr(){ return _Target; }
    public List<TimeLog> GetHistory(){ return _History; }


    // Methods
    public void AddToHistory( TimeLog PR )
    {
        GetHistory().Add(PR);
    }

    public void NewPr( string time )
    {
        _CurrentPR = new TimeLog( time );
        AddToHistory( _CurrentPR );
    }

    public void NewLog( string time )
    {
        AddToHistory( new TimeLog( time ) );
    }

    // Overrides 
    public override void CheckIn()
    {
        System.Console.WriteLine("Let's update your progress for this endurance goal!");
        System.Console.WriteLine("On your selected exercise, "+GetExercise()+", how what is your current time?");
        string GetTime = Console.ReadLine();

        double difference = FitnessGoal.GetDifference(
            FitnessGoal.StringsToDouble(
                new List<string>{ 
                    GetTime,
                    GetCurrentPr().GetTime()
        }));

        if( difference < 0 && double.Parse(GetTime) 
        < double.Parse(GetTargetPr().GetTime()))
        {
            NewPr( GetTime );
            CompleteGoal( difference.ToString() );
        } else if ( difference < 0 )
        {
            NewPr( GetTime );
            System.Console.WriteLine($"Amazing! You decreased your time by {difference.ToString("F2")}. You've reached a new PR!");
        } else
        {
            NewLog( GetTime );
            System.Console.WriteLine("Great job on reporting! Concistency is key.");
            System.Console.WriteLine("You've not yet reached a new PR, but keep at it!");
            System.Console.WriteLine($"The number to beat is {GetCurrentPr().GetTime()}.");
            
        }

    }
    public override void CompleteGoal( string difference )
    {
        System.Console.WriteLine("Congratulations! You've reached your goal.");
        System.Console.WriteLine($"You reported a new time of {GetCurrentPr().GetTime()}!");
        System.Console.WriteLine($"Thats a difference of {difference}."); // report lbs or kgs
        System.Console.WriteLine("Remember to enjoy your reward!");
        System.Console.WriteLine($"Reward: {GetReward()}");
        SetDateEnded();
        ToggleCompleted( true );
    }
    public override void PrintGoal()
    {
        System.Console.WriteLine("====================");
        System.Console.WriteLine($"Exercise: {GetExercise()}");
        System.Console.WriteLine($"Date Started: {GetEndDate()}");
        System.Console.WriteLine(
            IsComplete() ? $"Final PR: {GetCurrentPr().GetTime()}" 
                         : $"Current PR: {GetCurrentPr().GetTime()}"
        );
        System.Console.WriteLine($"Complete: {IsComplete()}");
        if( IsComplete() ) { System.Console.WriteLine($"End Date: {GetStartDate()}"); }

        System.Console.WriteLine();
        System.Console.WriteLine("Goal History:");

        GetHistory().ForEach( pr =>{ 
            System.Console.WriteLine($"     Date: {pr.GetDate()}");
            System.Console.WriteLine($"     Weight: {pr.GetTime()}");});
        System.Console.WriteLine("====================");
    }
}