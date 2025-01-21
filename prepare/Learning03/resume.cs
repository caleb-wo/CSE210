class Resume {
    public string name;
    public List<Job> jobs = new List<Job>();

    public void Display() 
    {
        Console.WriteLine($"{name}:");
        foreach (var job in jobs) 
        {
            Console.WriteLine($"{job.title} ({job.company}) {job.start}-{job.end}");
        }
    }
}