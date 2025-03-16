using System.Text.Json.Serialization;

[Serializable]
[JsonDerivedType(typeof(SmallGoal), typeDiscriminator: "small")]
[JsonDerivedType(typeof(MediumGoal), typeDiscriminator: "medium")]
[JsonDerivedType(typeof(LargeGoal), typeDiscriminator: "large")]
abstract class Goal
{
    [JsonInclude]
    protected User _user;
    [JsonInclude]
    protected string _GoalDescription;
    [JsonInclude]
    private bool _isDone = false;
    
    [JsonConstructorAttribute]
    public Goal(){}
    
    public Goal(User user, string desc)
    {
        _user = user;
        _GoalDescription = desc;
    }
    // GETTERS
    public User getUser(){ return _user; }
    public string getDescription(){ return _GoalDescription; }
    // SETTER
    public void IsDone(){ _isDone = true; }

    abstract public void Complete(); 

    public static string Stringify(Goal goal)
    {
        if(goal is SmallGoal)
        {
            SmallGoal sg = (SmallGoal) goal;
            return $"     Owner: {sg.getUser().getName()}\n     Value: {sg.getPointValue()}\n     Description: {sg.getDescription()}\n";
        } else if (goal is MediumGoal)
        {
            MediumGoal mg = (MediumGoal) goal;
            return $"     Owner: {mg.getUser().getName()}\n     Value: {mg.getPointValue()}\n     Description: {mg.getDescription()}\n";
        } else if (goal is LargeGoal)
        {
            LargeGoal lg = (LargeGoal) goal;
            return $"     Owner: {lg.getUser().getName()}\n     Value: {lg.getPointValue()}\n     Description: {lg.getDescription()}\n";
        } else { return "Error stringifying goal. Contact developer."; }
    }
}