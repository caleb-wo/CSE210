using System;

class Program
{
    static void Main(string[] args)
    {
     Job job1  = new Job();
     job1.company = "BYU-I";
     job1.title = "Cloud Architect";
     job1.start = 2002;
     job1.end = 2025;
     job1.Display();

     Resume resume1 = new Resume();
     resume1.name = "Mary Dettloff";
     resume1.jobs.Add(new Job());
     resume1.jobs[0].title = "Car Mechanic";
     resume1.jobs[0].company = "Mercedes Bens";
     resume1.jobs[0].start = 1998;
     resume1.jobs[0].end = 2015;
     resume1.jobs.Add(new Job());
     resume1.jobs[1].title = "Auto Mechanic";
     resume1.jobs[1].company = "Toyota";
     resume1.jobs[1].start = 2015;
     resume1.jobs[1].end = 3391;

     resume1.Display();
    }
}