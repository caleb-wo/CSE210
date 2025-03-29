using System.Diagnostics;
using System.Runtime.CompilerServices;

abstract class Goal
{
    // Constructor
    public Goal( int OwnerId
                ,string Description
                ,string Reward )
    {
        this._Owner = OwnerId;
        this._Description = Description;
        this._Reward = Reward;
        this._DateStarted = Goal.GetDate().ToString("MM-dd-yyyy");
    }
    // Fields
    private int _Owner;
    private string _Description;
    private string _Reward;
    private bool _IsComplete = false;
    private string _DateStarted;
    private string _DateEnded;
    // Static Methods
    public static DateTime GetDate()
    {
        DateTime now = DateTime.Now;
        return now;
    }
    // Setters
    public void SetDateEnded()
    {
        _DateEnded = Goal.GetDate().ToString("MM-dd-yyyy");
    }
    // Getters
    public int GetOwnerId() { return _Owner; }
    public string GetDescription() { return _Description; }
    public string GetReward() { return _Reward; }
    public bool IsComplete() { return _IsComplete; }
    public string GetStartDate() { return _DateStarted; }
    public string GetStartEnded() { return _DateEnded; } 
    // Abstract Methods
    public abstract void PrintGoal();
    public abstract void CompleteGoal();
}