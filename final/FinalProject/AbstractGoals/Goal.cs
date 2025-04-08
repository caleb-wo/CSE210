using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Goals 
{
    abstract class Goal
    {
        // Constructor
        public Goal( string OwnerId
                    ,string Description
                    ,string Reward )
        {
            this._Owner = OwnerId;
            this._Description = Description;
            this._Reward = Reward;
            this._DateStarted = Goal.GetDate().ToString("MM-dd-yyyy");
        }

        public Goal( string OwnerId
                    ,string Description
                    ,string Reward
                    ,bool IsComplete
                    ,string DateStarted 
                    ,string DateEnded )
        {
            this._Owner = OwnerId;
            this._Description = Description;
            this._Reward = Reward;
            this._IsComplete = IsComplete;
            this._DateStarted = DateStarted;
            this._DateEnded = DateEnded;
        }

        // Fields
        private string _Owner;
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

        public string GetCsvStarter()
        {
            return $"{GetOwnerId()}<><>{GetType().Name}<><>{GetDescription()}<><>{GetReward()}<><>{IsComplete()}<><>{GetStartDate()}<><>{GetEndDate()}";
        }

        // Setters
        public void SetDateEnded()
        {
            _DateEnded = Goal.GetDate().ToString("MM-dd-yyyy");
        
        }

        public void ToggleCompleted( bool TrueOrFalse)
        {
            _IsComplete = TrueOrFalse;
        }
        // Getters
        public string GetOwnerId() { return _Owner; }
        public string GetDescription() { return _Description; }
        public string GetReward() { return _Reward; }
        public bool IsComplete() { return _IsComplete; }
        public string GetStartDate() { return _DateStarted; }
        public string GetEndDate() { return _DateEnded; } 
        // Abstract Methods
        public abstract void PrintGoal();
        public abstract void CompleteGoal( string difference);
    }
}
