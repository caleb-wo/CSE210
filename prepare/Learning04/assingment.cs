class Assignment
{
    public Assignment(string n , string t)
    {
        this._name = n;
        this._topic = t;
    }
    private string _name;
    private string _topic;

    // GETTERS
    public string GetName()
    {
        return this._name;
    }
    public string GetTopic()
    {
        return this._topic;
    }

    public string GetSummary()
    {
        return $"Name: {this._name}\nTopic: {this._topic}";
    }
    // STATIC methods
    public static string GetName(Assignment a)
    {
        return a._name; 
    }
    public static string GetTopic(Assignment a)
        {
            return a._topic; 
        }
}