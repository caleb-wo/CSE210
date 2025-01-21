class Job 
{
    public string title;
    public string company;
    public int start;
    public int end;

    public void Display() {
        Console.WriteLine("===============");
        Console.WriteLine($"Title: {title}\nCompany: {company}\nStart/End: {start}-{end}");
        Console.WriteLine("===============");
    }
}