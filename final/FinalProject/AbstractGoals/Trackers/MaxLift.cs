namespace Tracker
{
    class MaxLift
    {
        private string _Weight;
        private string _Date;
        // Constructor
        public MaxLift( string Weight )
        {
            _Weight = Weight;
            _Date = Goal.GetDate().ToString();
        }
        // Getters
        public string GetWeight(){ return _Weight; }
        public string GetDate(){ return _Date; }
    }
}
