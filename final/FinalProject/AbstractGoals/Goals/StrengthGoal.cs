using Tracker;

class StrengthGoal : FitnessGoal
{
    // Fields
    private string _Lift;
    private MaxLift _CurrentPR;
    private MaxLift _Target;
    private List<MaxLift> _History;

    // Constructor
    public StrengthGoal( string OwnerId, string Description, 
                            string Reward, string Lift, 
                            string InitialPR, string TargetPR
    ) : base( OwnerId, Description, Reward)
    {
        _Lift = Lift;
        _CurrentPR = new MaxLift( InitialPR );
        _Target = new MaxLift( TargetPR );

        _History = new List<MaxLift>();
        _History.Add(_CurrentPR);
    }

    // CSV Constructor
    public StrengthGoal( string OwnerId, string Description, 
                            string Reward, bool IsComplete,
                            string DateStarted, string DateEnded,
                            string Lift, string InitialPR, string InitialPRDate,
                            string TargetPR, List<MaxLift> History
    ) : base( OwnerId, Description, Reward, IsComplete, DateStarted, DateEnded )
    {
        _Lift = Lift;
        _CurrentPR = new MaxLift( InitialPR, InitialPRDate );
        _Target = new MaxLift( TargetPR );

        _History = History;
    }

    // Save Goal
    public string ToCsv()
    {
        string HistoryCsv = "";

        if (GetHistory().Count > 0)
        {
            GetHistory().ForEach( log =>{
                HistoryCsv += $"<><>{log.GetWeight()}<><>{log.GetDate()}";
            });
        }
        

        return $"{GetCsvStarter()}<><>{GetLift()}<><>{GetCurrentPr().GetWeight()}<><>{GetCurrentPr().GetDate()}<><>{GetTargetPr().GetWeight()}<><>HISTORY_START{HistoryCsv}";
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
    public static StrengthGoal FromCsv(string csvLine)
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
        string currentWeight = parts[8];
        string currentDate = parts[9];
        string targetWeight = parts[10];

        // Parse history logs
        List<MaxLift> history = new List<MaxLift>();

        for (int i = historyStartIndex + 1; i < parts.Length - 1; i += 2)
        {
            string weight = parts[i];
            string date = parts[i + 1];
            history.Add(new MaxLift(weight, date));
        }

        // Create instance
        StrengthGoal goal = new StrengthGoal(
            ownerId,
            description,
            reward,
            isComplete,
            startDate,
            endDate,
            exercise,
            currentWeight,
            currentDate,
            targetWeight,
            history
        );

        return goal;
    }

    // Getters
    public string GetLift(){ return _Lift; }
    public MaxLift GetCurrentPr(){ return _CurrentPR; }
    public MaxLift GetTargetPr(){ return _Target; }
    public List<MaxLift> GetHistory(){ return _History; }


    // Methods
    public void AddToHistory( MaxLift PR )
    {
        GetHistory().Add(PR);
    }

    public void NewPr( string weight )
    {
        _CurrentPR = new MaxLift( weight );
        AddToHistory( _CurrentPR );
    }

    public void NewLog( string weight )
    {
        AddToHistory( new MaxLift( weight ) );
    }

    // Overrides 
    public override void CheckIn()
    {
        System.Console.WriteLine("Let's update your progress for this strength goal!");
        System.Console.WriteLine("On "+GetLift()+", how much weight were you able to lift?");
        string GetWeight = Console.ReadLine();

        double difference = FitnessGoal.GetDifference(
            FitnessGoal.StringsToDouble(
                new List<string>{ 
                    GetWeight,
                    GetCurrentPr().GetWeight()
        }));

        if( difference > 0 && double.Parse(GetWeight) 
        > double.Parse(GetTargetPr().GetWeight()))
        {
            NewPr( GetWeight );
            CompleteGoal( difference.ToString() );
        } else if ( difference > 0 )
        {
            NewPr( GetWeight );
            System.Console.WriteLine($"Amazing! You increased your max by {difference.ToString("F2")}. You've reached a new PR!");
        } else
        {
            NewLog( GetWeight );
            System.Console.WriteLine("Great job on reporting! Concistency is key.");
            System.Console.WriteLine("You've not yet reached a new PR, but keep at it!");
            System.Console.WriteLine($"The number to beat is {GetCurrentPr().GetWeight()}.");
            
        }

    }
    public override void CompleteGoal( string difference )
    {
        System.Console.WriteLine("Congratulations! You've reached your goal.");
        System.Console.WriteLine($"You reported a new max of {GetCurrentPr().GetWeight()}!");
        System.Console.WriteLine($"Thats a difference of {difference}."); // report lbs or kgs
        SetDateEnded();
        ToggleCompleted( true );
    }
    public override void PrintGoal()
    {
        System.Console.WriteLine("====================");
        System.Console.WriteLine($"Lift: {GetLift()}");
        System.Console.WriteLine($"Date Started: {GetEndDate()}");
        System.Console.WriteLine(
            IsComplete() ? $"Final PR: {GetCurrentPr().GetWeight()}" 
                         : $"Current PR: {GetCurrentPr().GetWeight()}"
        );
        System.Console.WriteLine($"Complete: {IsComplete()}");
        if( IsComplete() ) { System.Console.WriteLine($"End Date: {GetStartDate()}"); }

        System.Console.WriteLine();
        System.Console.WriteLine("Goal History:");

        GetHistory().ForEach( pr =>{ 
            System.Console.WriteLine($"     Date: {pr.GetDate()}");
            System.Console.WriteLine($"     Weight: {pr.GetWeight()}");});
        System.Console.WriteLine("====================");
    }
}