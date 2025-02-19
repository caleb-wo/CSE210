class MathAssignment : Assignment
{
    public MathAssignment(string n, string t, string tb, string p) : base(n,t)
    {
        this._textbookSection = tb;
        this._problems = p;
    }
    private string _textbookSection;
    private string _problems;

    public string GetHomeworkList()
    {
        return $"Textbook: {this._textbookSection}\nProblems: {this._problems}";
    }
}