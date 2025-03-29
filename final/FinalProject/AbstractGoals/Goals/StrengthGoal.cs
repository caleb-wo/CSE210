using Tracker;

class StrengthGoal : FitnessGoal
{
    // Fields
    private string _Lift;
    private MaxLift _CurrentPR;
    private List<MaxLift> _History;

    // Constructor
    public StrengthGoal( int OwnerId, string Description, 
                            string Reward, string Weight, 
                            string Height, int Age, 
                            bool IsMale, string Lift, 
                            string InitialPR
    ) : base( OwnerId, Description, 
                Reward, Weight, 
                Height, Age, IsMale
    )
    {
        _Lift = Lift;
        _CurrentPR = new MaxLift( InitialPR );

        _History = new List<MaxLift>();
        _History.Add(_CurrentPR);
    }

    // Getters
    public string GetLift(){ return _Lift; }
    public MaxLift GetCurrentPr(){ return _CurrentPR; }
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

        if(difference > 0)
        {
            System.Console.WriteLine($"Amazing! You increased your max by {difference:.2f}. You've reached a new PR!");
            NewPr( GetWeight );
        } else
        {
            System.Console.WriteLine("Great job on reporting! Concistency is key.");
            System.Console.WriteLine("You've not yet reached a new PR, but keep at it!");
            System.Console.WriteLine($"The number to beat is {GetCurrentPr().GetWeight()}.");
            
        }

    }
    public override void CompleteGoal()
    {
    }
    public override void PrintGoal()
    {
    }
}