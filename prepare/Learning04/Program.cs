using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Begin Assignment Object tests");


        Console.WriteLine("Base Assignment Object:");
        Assignment a = new Assignment("Caleb", "Spring Boot Development");
        Console.WriteLine(a.GetSummary());
        
        Console.WriteLine("Math Assignment Object:");
        MathAssignment mA = new MathAssignment("Caleb", "Math Class", "Section 3", "13-20");
        Console.WriteLine(mA.GetSummary());
        Console.WriteLine(mA.GetHomeworkList());

        Console.WriteLine("WritingAssignment Object:");
        WritingAssignment wA = new WritingAssignment("Caleb", "Writing Class", "Alexander The Great: In Depth");
        Console.WriteLine(wA.GetSummary());
        Console.WriteLine(wA.GetWritingInformation());
    }
}