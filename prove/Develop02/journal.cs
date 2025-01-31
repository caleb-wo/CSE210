using System;
using System.IO;
using System.Collections.Generic;
class Journal
{
    private List<Entry> entries = new List<Entry>();

    //ADD and entry to this journal.
    public void addEntry(String prompt)
    {
        Entry ent = new Entry();
        Console.WriteLine("Date MM/DD/YYYY: ");
        String date = Console.ReadLine();

        Console.WriteLine(prompt);
        String text = Console.ReadLine();

        ent.setDate(date);
        ent.setPrompt(prompt);
        ent.setText(text);

        entries.Add(ent);
    }
    //LOAD entries from a file.
    public void loadEntry(string date, string prompt, string text)
    {
        Entry ent = new Entry();
        ent.setDate(date);
        ent.setPrompt(prompt);
        ent.setText(text);

        entries.Add(ent);
    }
    //BUILD journal based on a file.
    public void buildPastEntries(String filepath)
    {
        if (string.IsNullOrWhiteSpace(filepath)) //CHECK for null or whitespace filename.
        {
            Console.WriteLine("Invalid file.");
            return;
        }
        else
        {
            using (StreamReader reader = new StreamReader(filepath)) //Open file for reading.
            {
                string line;
                while ((line = reader.ReadLine()) != null) //GO until empty line.
                {
                    string[] entryData = line.Split("|||"); //SPLIT data in to a 3 item array. Then give each for loidEntry()
                    if (entryData.Length == 3)
                    {
                        loadEntry(entryData[0], entryData[1], entryData[2]);
                    }
                    else
                    {
                        Console.WriteLine("Line reading error.");
                    }
                }
            }
        }
    }

    public void listEntries() //LIST entries. Numbered.
    {
        int count = 1;
        foreach (Entry ent in entries)
        {
            Console.WriteLine($"({count})--------{ent.getDate()}");
            Console.WriteLine($"Prompt: {ent.getPrompt()}");
            Console.WriteLine($"Entry: {ent.getText()}");
            ++count;
        }
    }

    public void printEntry(int idx) //Print one entry to console.
    {
        Console.WriteLine($"Entry #{idx + 1}");
        Console.WriteLine($"Date: {entries[idx].getDate()}");
        Console.WriteLine($"Prompt: {entries[idx].getPrompt()}");
        Console.WriteLine($"Response: {entries[idx].getText()}");
    }

    public void publishJournal(string filename) //Write all current entries to a file.
    {
        using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(fs)) //Open file and get writer.
        {
            foreach (Entry ent in entries)
            {
                writer.WriteLine($"{ent.getDate()}|||{ent.getPrompt()}|||{ent.getText()}");
            }
        }
    }

    public List<Entry> getEntries()
    {
        return this.entries;
    }
}