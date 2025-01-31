using System.Runtime.InteropServices.Marshalling;

class Manager
{
    //CREATE user's journal for thing journalling session.
    Journal user = new Journal(); 
    private Boolean isJournaling = true;
    //LIST prompts for the user.
    private List<string> prompts = new List<string>{
        "Who was the most interesting person I interacted with today?"
        ,"What was the best part of my day?"
        ,"How did I see the hand of the Lord in my life today?"
        ,"What was the strongest emotion I felt today?"
        ,"If I had one thing I could do over today, what would it be?"
        ,"What was something unique that happened today?"
        ,"How did I manage my emotions today?"
        ,"What was the most interesting thing someone said to me today?"
        ,"What are some things that you were grateful for today?"
        ,"FREE FOR ALL. Write what you want!"
        ,"How did you make today a good day?"
    };
    //MAIN journaling loop that services the user.
    public void manageUser()
    {
        Console.WriteLine("Welcome to your journaling program!");
        Console.WriteLine("Are you a new user?");
        string userStatus = Console.ReadLine();
        if (userStatus == "yes" || userStatus == "YES" || userStatus == "y"|| userStatus == "Y")
        {
            do
            {
                //DISPLAY the menu and get the user's option. Then,
                //  using a switch statement check the value and do
                //  the appropriate process for the user.
                displayMenu();
                Console.WriteLine("Select option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": //BUILD past entries and then list all of the entries loaded.
                        Console.WriteLine("Enter filename/path: ");
                        string filename = Console.ReadLine();
                        user.buildPastEntries(filename);
                        user.listEntries();
                        break;
                    case "2": //LISTS all currently loaded entries. Checks for null.
                        if (user.getEntries().Count != 0)
                        {
                            user.listEntries();
                        }
                        else { Console.WriteLine("No entries found. Try loading journal."); }
                        break;
                    case "3": //DISPLAY a specific entry.
                        user.listEntries();
                        Console.WriteLine("Select an entry: ");
                        string getIdx = Console.ReadLine();
                        int idx = int.Parse(getIdx) - 1;
                        user.printEntry(idx);
                        break;
                    case "4": //ADD entry to the journal.
                        user.addEntry(getRandPrompt());
                        break;
                    case "5": //Save the journal to a file and end the session.
                        Console.WriteLine("Enter filename/path: ");
                        string fileSave = Console.ReadLine();
                        user.publishJournal(fileSave);
                        setJournaling(false); //BOOLEAN too false. See do {} while.
                        break;
                    default: //INVALID number.
                        Console.WriteLine("Invalid selection.");
                        break;
                }

            } while (getJournaling());
        } else 
        {
            manageUserReturning();
        }
    }
    // MAIN loop for a returning user.
    public void manageUserReturning()
    {
        Console.WriteLine("Welcome to your journaling program!");
        Console.WriteLine("Enter your journal data file: ");
        string filename = Console.ReadLine();
        do
        {
            //DISPLAY the menu and get the user's option. Then,
            //  using a switch statement check the value and do
            //  the appropriate process for the user.
            displayMenu();
            Console.WriteLine("Select option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": //BUILD past entries and then list all of the entries loaded.
                    Console.WriteLine("Building journal...");
                    user.buildPastEntries(filename);
                    user.listEntries();
                    break;
                case "2": //LISTS all currently loaded entries. Checks for null.
                    if (user.getEntries().Count != 0)
                    {
                        user.listEntries();
                    }
                    else { Console.WriteLine("No entries found. Try loading journal."); }
                    break;
                case "3": //DISPLAY a specific entry.
                    user.listEntries();
                    Console.WriteLine("Select an entry: ");
                    string getIdx = Console.ReadLine();
                    int idx = int.Parse(getIdx) - 1;
                    user.printEntry(idx);
                    break;
                case "4": //ADD entry to the journal.
                    user.addEntry(getRandPrompt());
                    break;
                case "5": //Save the journal to a file and end the session.
                    Console.WriteLine("Saving journal...\nGoodbye!");
                    string fileSave = Console.ReadLine();
                    user.publishJournal(fileSave);
                    setJournaling(false); //BOOLEAN too false. See do {} while.
                    break;
                default: //INVALID number.
                    Console.WriteLine("Invalid selection.");
                    break;
            }

        } while (getJournaling());
    }
    public void displayMenu()
    {
        Console.WriteLine("|==============MENU==============|");
        Console.WriteLine("1. Load Journal");
        Console.WriteLine("2. List Entries");
        Console.WriteLine("3. Show Entry");
        Console.WriteLine("4. Add Entry");
        Console.WriteLine("5. Publish Journal (save & quit)");
        Console.WriteLine("|================================|");
    }

    public void setJournaling(Boolean tf)
    {
        this.isJournaling = tf;
    }
    public Boolean getJournaling()
    {
        return this.isJournaling;
    }

    public string getRandPrompt()
    {
        Random r = new Random();
        int randIdx = r.Next(0, prompts.Count);

        return prompts[randIdx];
    }
}