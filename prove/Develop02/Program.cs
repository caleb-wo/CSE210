using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine("Hello Develop02 World!");
        GuessGame();
        NumberList();
        DisplayTool();
    }

    static void GuessGame() {
        Random getRand = new Random();
        int magicNum = getRand.Next(1, 101);
        bool guess = false;
        do 
        {
            Console.Write("What is your guess? ");
            string response = Console.ReadLine();
            int answer = int.Parse(response);

            if (answer > magicNum)
            {
                Console.WriteLine("Lower");
            } 
            else if (answer < magicNum)
            {
                Console.WriteLine("Higher");
            }
            else
            {
                Console.WriteLine("You guessed it!");
                guess = true;
            }

        } while (!guess);

    }
    static void NumberList() {
        List<int> numbers = new List<int>();
        Console.WriteLine("Please enter number. To stop, press '0'");
        string getNum = "";
        int number = 1000;
        while (number != 0)
        {
            Console.Write("Number: ");
            getNum = Console.ReadLine();
            number = int.Parse(getNum);
            if (number != 0)
            {
                numbers.Add(number);
            }
        }
        int sum = numbers.Sum();
        int max = numbers.Max();
        Console.WriteLine($"Sum: {sum}\nMax: {max}");
    }
    static void DisplayTool() {
        DisplayWelcome();
        string name = PromptUserName();
        int userNumber = PromptUserNumber();
        float finalNum = SquareNumber(userNumber);
        DisplayResult(name , finalNum);

        static void DisplayWelcome() {
            Console.WriteLine("Welcome to the program!");
        }
        static string PromptUserName() {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            return name;
        }
        static int PromptUserNumber() {
            Console.Write("Number: ");
            string getNum = Console.ReadLine();
            int number = int.Parse(getNum);
            return number;
        }
        static float SquareNumber(int flatNum) {
            Console.Write("Exponent: ");
            string getExp = Console.ReadLine();
            float exp = float.Parse(getExp);
            float number = flatNum;
            float expNum = MathF.Pow(number , exp);
            return expNum;
        }
        static void DisplayResult(string name , float number) {
            Console.WriteLine($"{name}, the exponent of your number is {number}.");
        }
    }
}