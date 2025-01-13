using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What grade did you get (e.g. 88.45)? ");
        string stringGrade = Console.ReadLine();
        float grade = float.Parse(stringGrade);
        string letterGrade;

        /* 100-90, 89-80, 79-70, 69-60, 59-50 */
        if (grade >= 90)
        {
            letterGrade = "A";
        }
        else if ((grade <= 89) && (grade >= 80))
        {
            letterGrade = "B";
        }
        else if ((grade <= 79) && (grade >= 70))
        {
            letterGrade = "C";
        }
        else if ((grade <= 69) && (grade >= 60))
        {
            letterGrade = "D";
        }
        else if (grade < 60)
        {
            letterGrade = "F";
        }
        else
        {
            Console.WriteLine("ERROR. Invalid grade.");
            return;
        }
        Console.WriteLine($"You earned a {letterGrade}");
        if (grade >= 70)
        {
            Console.Write("Congratulations! You passed the class.");
        }
    }
}