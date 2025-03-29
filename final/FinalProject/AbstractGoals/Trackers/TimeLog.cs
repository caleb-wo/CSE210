namespace Tracker
{
    class TimeLog
    {
        private string _Time;
        private string _Date;
        // Constructor
        public TimeLog( string Time )
        {
            _Time = Time;
            _Date = Goal.GetDate().ToString();
        }
        // Getters
        public string GetTime(){ return _Time; }
        public string GetDate(){ return _Date; }
    }
}