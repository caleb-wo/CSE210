using Goals;

namespace Tracker
{
    class WeightLog
    {
        private string _Weight;
        private string _Date;
        // Constructor
        public WeightLog( string Weight )
        {
            _Weight = Weight;
            _Date = Goal.GetDate().ToString();
        }
        public WeightLog( string Weight, string Date )
        {
            _Weight = Weight;
            _Date = Date;
        }        
        
        // Getters
        public string GetWeight(){ return _Weight; }
        public string GetDate(){ return _Date; }
    }
}