using Tracker;

class WeightLossGoal : FitnessGoal
{
    // Fields
    private WeightLog _CurrentPrWeight;
    private WeightLog _Target;
    private List<WeightLog> _History;

    // Constructor
    public WeightLossGoal( string OwnerId, string Description, 
                            string Reward,
                            string InitialWeight,
                            string TargetWeight
    ) : base( OwnerId, Description, Reward )
    {
        _CurrentPrWeight = new WeightLog( InitialWeight );
        _Target = new WeightLog( TargetWeight );

        _History = new List<WeightLog>();
        _History.Add(_CurrentPrWeight);
    }

    // CSV Constructor
    public WeightLossGoal( string OwnerId, string Description, 
                            string Reward, bool IsComplete,
                            string DateStarted, string DateEnded,
                            string InitialPR, string InitialPRDate,
                            string TargetPR, List<WeightLog> History
    ) : base( OwnerId, Description, Reward, IsComplete, DateStarted, DateEnded )
    {
        _CurrentPrWeight = new WeightLog( InitialPR, InitialPRDate );
        _Target = new WeightLog( TargetPR );

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
        

        return $"{GetCsvStarter()}<><>Not Applicable<><>{GetCurrentPrWeight().GetWeight()}<><>{GetCurrentPrWeight().GetDate()}<><>{GetTarget().GetWeight()}<><>HISTORY_START{HistoryCsv}";
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
    public static WeightLossGoal FromCsv(string csvLine)
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
        string currentWeight = parts[8];
        string currentDate = parts[9];
        string targetWeight = parts[10];

        // Parse history logs
        List<WeightLog> history = new List<WeightLog>();

        for (int i = historyStartIndex + 1; i < parts.Length - 1; i += 2)
        {
            string weight = parts[i];
            string date = parts[i + 1];
            history.Add(new WeightLog(weight, date));
        }

        // Create instance
        WeightLossGoal goal = new WeightLossGoal(
            ownerId,
            description,
            reward,
            isComplete,
            startDate,
            endDate,
            currentWeight,
            currentDate,
            targetWeight,
            history
        );

        return goal;
    }

    // Getters
    public WeightLog GetCurrentPrWeight(){ return _CurrentPrWeight; }
    public WeightLog GetTarget(){ return _Target; }
    public List<WeightLog> GetHistory(){ return _History; }


    // Methods
    public void AddToHistory( WeightLog PR )
    {
        GetHistory().Add(PR);
    }

    public void NewPr( string weight )
    {
        _CurrentPrWeight = new WeightLog( weight );
        AddToHistory( _CurrentPrWeight );
    }

    public void NewLog( string weight )
    {
        AddToHistory( new WeightLog( weight ) );
    } 

    // Overrides 
    public override void CheckIn()
    {
        System.Console.WriteLine("Let's check your weightloss progess!");
        System.Console.WriteLine("How much do you weigh currently?");
        string GetWeight = Console.ReadLine();

        double difference = FitnessGoal.GetDifference(
            FitnessGoal.StringsToDouble(
                new List<string>{ 
                    GetWeight,
                    GetCurrentPrWeight().GetWeight()
        }));

        if( 
            difference < 0 && double.Parse(GetWeight) 
            < double.Parse(GetTarget().GetWeight())
        )
        {
            NewPr( GetWeight );
            CompleteGoal( difference.ToString() );
        } else if ( difference < 0 )
        {
            NewPr( GetWeight );
            System.Console.WriteLine($"Amazing! You lost approximately {difference.ToString("F2")}. You've reached a new low!");
        } else
        {
            NewLog( GetWeight );
            System.Console.WriteLine("Great job on reporting! Concistency is key.");
            System.Console.WriteLine("You've not yet reached your target weight, but keep at it!");
            System.Console.WriteLine($"The weight to get under is {GetCurrentPrWeight().GetWeight()}.");
            
        }

    }
    public override void CompleteGoal( string difference )
    {
        System.Console.WriteLine("Congratulations! You've reached your goal.");
        System.Console.WriteLine($"You reported a new weight of {GetCurrentPrWeight().GetWeight()}!");
        System.Console.WriteLine($"Thats a difference of {difference}."); // report lbs or kgs
        SetDateEnded();
        ToggleCompleted( true );
    }
    public override void PrintGoal()
    {
        System.Console.WriteLine("====================");
        System.Console.WriteLine($"Date Started: {GetEndDate()}");
        System.Console.WriteLine($"Initial Weight: {GetHistory()[0].GetWeight()}");
        System.Console.WriteLine
        (
            IsComplete() ? $"Final Weight: {GetCurrentPrWeight().GetWeight()}" 
                         : $"Current PR: {GetCurrentPrWeight().GetWeight()}"
        );
        System.Console.WriteLine($"Complete: {IsComplete()}");
        if( IsComplete() ) { System.Console.WriteLine($"End Date: {GetStartDate()}"); }

        System.Console.WriteLine();
        System.Console.WriteLine("Goal History:");

        GetHistory().ForEach( weight =>
        { 
            System.Console.WriteLine($"     Date: {weight.GetDate()}");
            System.Console.WriteLine($"     Weight: {weight.GetWeight()}");
        });

        System.Console.WriteLine("====================");
    }
}