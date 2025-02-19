class WritingAssignment : Assignment
{
    public WritingAssignment(string n , string t , string ti) : base(n , t)
    {
        this._title = ti;
    }
    
    private string _title;

    public string GetWritingInformation()
    {
        return $"{Assignment.GetName(this)} – {Assignment.GetTopic(this)}\n{this._title}";
    }
}