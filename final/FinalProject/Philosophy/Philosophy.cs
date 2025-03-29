using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;


class Philosophy
{
    // Fields
    private string _Philosophy;
    private string _Purpose;
    private string _Slogan;
    private bool _IsComplete;
    private string _DateStarted;
    private string _DateEnded;

    // Setters
    public void SetDateEnded()
    {
        _DateEnded = GetDateTime().ToString("MM-dd-yyyy");
    }

    // Getters
    public string GetPhilosophy(){ return _Philosophy; }
    public string GetPurpose(){ return _Purpose; }
    public string GetSlogan(){ return _Slogan; }
    public bool GetCompleted(){ return _IsComplete; }
    public string GetDateStarted(){ return _DateStarted; }
    public string GetDateEnded(){ return _DateEnded; }

    // Constructor
    public Philosophy(
        string Philosophy,
        string Purpose,
        string Slogan)
    {
        _Philosophy = Philosophy;
        _Purpose = Purpose;
        _Slogan = Slogan;
        _IsComplete = false;
        _DateStarted = GetDateTime().ToString("MM-dd-yyyy");
    }

    // Static Methods
    public static DateTime GetDateTime() 
    {
        DateTime now = DateTime.Now; 
        return now;
    }

    // Methods
    public void PrintPhilosophy() 
    {   
        System.Console.WriteLine("============");
        System.Console.WriteLine("Slogan: "+GetSlogan());
        System.Console.WriteLine("Purpose: "+GetPurpose());
        System.Console.WriteLine("Start Date: "+GetDateStarted());
        if (_IsComplete) { System.Console.WriteLine("End Date: "+GetDateEnded()); }
        System.Console.WriteLine("\nPhilosophy: "+GetPhilosophy());
        System.Console.WriteLine("============");
    }

}