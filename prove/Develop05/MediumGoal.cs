using System.Text.Json.Serialization;

[Serializable]
class MediumGoal : Goal
{
    [JsonInclude]
    protected int _pointValue;
    public int getPointValue(){ return _pointValue;}

    public MediumGoal() : base() { } 
    
    public MediumGoal(User u, string desc) : base(u, desc)
    {
        _pointValue = 3000;
    }

    public override void Complete()
    {
        IsDone();
        getUser().addPoints(_pointValue);
        getUser().Finish(this);
        System.Console.WriteLine("Well done! You gained "+_pointValue+" points!");
    }
}