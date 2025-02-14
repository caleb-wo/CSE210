using System;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


class Program
{
    static void Main(string[] args)
    {
        string filePath = "./lds-scriptures.json";
        Book myLibrary = new Book();
        using (StreamReader reader =  new StreamReader(filePath))
        using (JsonTextReader JSONread = new JsonTextReader(reader))
        {
            JArray AllText = JArray.Load(JSONread);
            foreach (JObject verse in AllText)
            {
                string verseText = verse["scripture_text"].ToString();
                string verseRef = verse["verse_title"].ToString();
                myLibrary.addScripture(
                    new Scripture(verseText , verseRef)
                );
            }
        }
        // Scripture s = new Scripture(
        //     "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths."
        //     ,"Proverbs 3: 5-6"
        // );
        Console.Clear();
        Console.WriteLine("What is the verse reference? (Genesis 3:12)");
        string refer = Console.ReadLine();
        Scripture s = myLibrary.getScripture(refer);
        while (!s.checkHidden() && !s.getForceQuit())
        {
            Console.Clear();
            Console.WriteLine(s.getRef().getRef());
            Console.WriteLine("=====================");
            s.printVerses();
            Console.WriteLine("=====================");
            Console.WriteLine();
            Console.WriteLine("Type 'quit' to close. Click [enter] to continue.");
            string userInp = Console.ReadLine().ToLower();

            if (userInp == "quit")
            {
                s.forceQuit();
            } else if (userInp == "")
            {
                s.randomizeScripture();
            } else
            {
                Console.WriteLine("Invalid input.");
                Thread.Sleep(1500);
            }
        }
        Console.Clear();
        Console.WriteLine(s.getRef().getRef());
        Console.WriteLine("=====================");
        s.printVerses();
        Console.WriteLine("=====================");
        Console.WriteLine();
        Console.WriteLine("Goodbye.");
    }
}