using Goals;
using Philosophies;
using System.Text.RegularExpressions;
using System.Globalization;


class User 
{
    private User(){}
    // Fields
    private Philosophy _Philosophy;
    private string _UserId = Guid.NewGuid().ToString();
    private List<Goal> _Goals = new List<Goal>();
    private int _Age;
    private bool _IsMale;
    private bool _IsImperial;
    private double _Weight;
    private double _Height;
    private string _Birth;
    private string _Name;
    // Static Fields
    public static TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;


    // CSV Constructor
    public User( string Philosophy, string Purpose, string Slogan, bool IsComplete, string Start,
                            string UserId, int Age, bool IsMale,
                            bool IsImperial, double Weight,
                            double Height, string Birth, string Name,
                            List<Goal> goals )
    {
        // Initialize Philosophy field using a new Philosophy object
        _Philosophy = new Philosophy(Philosophy, Purpose, Slogan, IsComplete, Start);
        
        // Initialize other fields directly from constructor parameters
        _UserId = UserId;
        _Age = Age;
        _IsMale = IsMale;
        _IsImperial = IsImperial;
        _Weight = Weight;
        _Height = Height;
        _Birth = Birth;
        _Name = Name;
        _Goals = goals;
    }

    // Getters
    public Philosophy GetPhilosophy(){ return _Philosophy; }
    public string GetUserId() { return _UserId; }
    public List<Goal> GetGoals() { return _Goals; }
    public int GetAge() { return _Age; }
    public bool GetIsMale() { return _IsMale; }
    public bool GetIsImperial() { return _IsImperial; }
    public double GetWeight() { return _Weight; }
    public double GetHeight() { return _Height; }
    public string GetBirth() { return _Birth; }
    public string GetName() { return _Name; }

    // Setters
    public void SetPhilosophy(Philosophy philosophy) { _Philosophy = philosophy; }
    public void SetUserId(string userId) { _UserId = userId; }
    public void SetGoals(List<Goal> goals) { _Goals = goals; }
    public void SetAge(int age) { _Age = age; }
    public void SetIsMale(bool isMale) { _IsMale = isMale; }
    public void SetIsImperial(bool isImperial) { _IsImperial = isImperial; }
    public void SetWeight(double weight) { _Weight = weight; }
    public void SetHeight(double height) { _Height = height; }
    public void SetBirth(string birth) { _Birth = birth; }
    public void SetName(string name) { _Name = name; }


    // Psuedo Getters
    public List<Goal> GetActiveGoals()
    {
        return GetGoals().Where(goal => !goal.IsComplete()).ToList();
    }

    public List<Goal> GetInactiveGoals()
    {
        return GetGoals().Where(goal => goal.IsComplete()).ToList();
    }


    // Factory Methods
    public static User InitNewUser()
    {
        // Get string variables 
        System.Console.WriteLine("Philosophy: ");
        string GetPhilosophy = Console.ReadLine().Trim();
        System.Console.WriteLine("Purpose: ");
        string GetPurpose = Console.ReadLine().Trim();
        System.Console.WriteLine("Slogan: ");
        string GetSlogan = Console.ReadLine().Trim();

        System.Console.WriteLine("Age: ");
        string GetAgeStr = Console.ReadLine().Trim();

        System.Console.WriteLine("Sex (M/F): ");
        string GetSex = Console.ReadLine().Trim().ToUpper();
        
        System.Console.WriteLine("Imperial or Metric (I/M): ");
        string GetSystem = Console.ReadLine().Trim().ToUpper();

        System.Console.WriteLine("For weight, use the measurement that corresonds with the prior answer (lbs or kg).");
        System.Console.WriteLine("For height, use total inches or centimeters. Only use 2 digits pass the decimal (e.g. 71.29, which is 5'9\")");
        System.Console.WriteLine("Weight: ");
        string GetWeightStr = Console.ReadLine().Trim();

        System.Console.WriteLine("Height: ");
        string GetHeightStr = Console.ReadLine().Trim();

        System.Console.WriteLine("Birth (MM-dd-yyyy): ");
        string GetBirth = Console.ReadLine().Trim();

        System.Console.WriteLine("Name: ");
        string GetName = textInfo.ToTitleCase( Console.ReadLine().Trim() );
        
        // Validation
        string WholeNumberUnder150 = @"^(?:[0-9]{1,2}|1[0-4][0-9])$";
        string WeightPattern = @"^([0-9]{1,3})(\.[0-9]{1,6})?$"; // up to 999 before the decimal and then up to 6 digits after the decimal.
        string HeightPattern = @"^(?:[0-9]{1,2}|[1-3][0-9]{2}|400)(\.[0-9]{1,2})?$"; // Maxes out after 400 and takes 2 digits after the decimal.

        bool ValidAge = Regex.IsMatch( GetAgeStr, WholeNumberUnder150 );
        bool ValidWeight = Regex.IsMatch( GetWeightStr, WeightPattern );
        bool ValidHeight = Regex.IsMatch( GetHeightStr, HeightPattern );
        if (!ValidAge || !ValidWeight || !ValidHeight)
        {
            Console.WriteLine("Invalid input. Please reference the following error message(s).");

            List<string> StatusCodes = new List<string>
            {
                 ValidAge ? $"Age: {GetAgeStr}" : $"Age: ERROR --- Please input a whole number."
                ,ValidWeight ? $"Weight: {GetWeightStr}" : $"Weight: ERROR --- Please input a number under 1,000. If it's a decmial, ensure it has 6 or less digits after the decimal point."
                ,ValidHeight ? $"Height: {GetHeightStr}" : $"Height: ERROR --- Please input your height in a number matching your measurement preference (inches for imperial & centimeters for metric). It must be under 400 and have only 2 digits after the decimal point if applicable."
            };
            StatusCodes.ForEach( code => System.Console.WriteLine(code));

            Environment.Exit(0);
        }

        // Populate instance fields
        User newUser = new User();
        newUser.SetPhilosophy( new Philosophy( GetPhilosophy, GetPurpose, GetSlogan ) );
        newUser.SetAge( int.Parse(GetAgeStr) );
        newUser.SetIsMale( GetSex == "M" ? true : false );
        newUser.SetIsImperial( GetSystem == "I" ? true : false );
        newUser.SetWeight( !newUser.GetIsImperial() ? double.Parse( GetWeightStr ) : FitnessMetrics.LbsToKgs( double.Parse( GetWeightStr ) ));
        newUser.SetHeight( !newUser.GetIsImperial() ? double.Parse( GetHeightStr ) : FitnessMetrics.InToCm( double.Parse( GetHeightStr ) ));
        newUser.SetBirth( GetBirth );
        newUser.SetName( GetName );
    
        return newUser;
    }

    // To CSV
    public string ToCsv()
    {
        // Philosophy<><>Purpose<><>Slogan<><>Completed<><>StartDate<><>UserId<><>Age<><>Sex<><>Measurement<><>Weight<><>Height<><>Birth<><>Name
        return $"{GetPhilosophy().GetPhilosophy()}<><>{GetPhilosophy().GetPurpose()}<><>{GetPhilosophy().GetSlogan()}<><>{GetPhilosophy().GetCompleted()}<><>{GetPhilosophy().GetDateStarted()}<><>{GetUserId()}<><>{GetAge()}<><>{GetIsMale()}<><>{GetIsImperial()}<><>{GetWeight()}<><>{GetHeight()}<><>{GetBirth()}<><>{GetName()}";
    }

    // Save User
    public void SaveUserToCsv()
    {
        // Define the directory and file path
        string csvDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv");
        string path = Path.Combine(csvDirectory, "users.csv");
        
        // Create the directory if it doesn't exist
        if (!Directory.Exists(csvDirectory))
        {
            Directory.CreateDirectory(csvDirectory);
        }
        
        string userId = GetUserId();
        List<string> lines = new List<string>();
        
        if (File.Exists(path))
        {
            lines = File.ReadAllLines(path).ToList();
        }
        else
        {
            // Header if file doesn't exist
            lines.Add("Philosophy<><>Purpose<><>Slogan<><>Completed<><>StartDate<><>UserId<><>Age<><>Sex<><>Measurement<><>Weight<><>Height<><>Birth<><>Name");
        }
        
        // Find and update existing user, if present
        bool updated = false;
        for (int i = 1; i < lines.Count; i++) // Skip header
        {
            if (lines[i].Contains($"<><>{userId}<><>"))
            {
                lines[i] = ToCsv(); // Replace with new data
                updated = true;
                break;
            }
        }
        
        if (!updated)
        {
            lines.Add(ToCsv());
        }
        
        File.WriteAllLines(path, lines);
    }


    public static List<Goal> LoadGoals(string userId)
    {
        // Define the directory and file path
        string csvDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv");
        string path = Path.Combine(csvDirectory, "goals.csv");
        
        // Check if directory exists first
        if (!Directory.Exists(csvDirectory))
        {
            Console.WriteLine("CSV directory not found.");
            return new List<Goal>();
        }

        // Check if file exists
        if (!File.Exists(path))
        {
            Console.WriteLine("Goals file not found.");
            return new List<Goal>();
        }

        List<Goal> goalsFromCsv = new List<Goal>();
        // Read all lines from the CSV
        var lines = File.ReadAllLines(path);
        // Iterate through each line in the file
        foreach (var line in lines)
        {
            if (line.StartsWith("OwnerId")) 
                continue;
            
            string[] parts = line.Split(new string[] { "<><>" }, StringSplitOptions.None);
            if (parts[0] == userId)
            {
                switch (parts[1])
                {
                    case "EnduranceGoal":
                        goalsFromCsv.Add(EnduranceGoal.FromCsv(line));
                        break;
                    case "StrengthGoal":
                        goalsFromCsv.Add(StrengthGoal.FromCsv(line));
                        break;
                    case "WeightLossGoal":
                        goalsFromCsv.Add(WeightLossGoal.FromCsv(line));
                        break;
                }
            }
        }
        
        return goalsFromCsv;
    }


    // User from CSV
    public static User FromCsv(string csvLine)
    {
        string[] parts = csvLine.Split(new string[] { "<><>" }, StringSplitOptions.None);

        // Parse fields from the CSV line
        string philosophy = parts[0];
        string purpose = parts[1];
        string slogan = parts[2];
        bool completed = bool.Parse(parts[3]);
        string start = parts[4];
        string userId = parts[5];
        int age = int.Parse(parts[6]);
        bool isMale = bool.Parse(parts[7]);
        bool isImperial = bool.Parse(parts[8]);
        double weight = double.Parse(parts[9]);
        double height = double.Parse(parts[10]);
        string birth = parts[11];
        string name = parts[12];

        // Create and return a new User instance
        User user = new User(philosophy, purpose, slogan, completed, start, userId, age, 
        isMale, isImperial, weight, height, birth, name, User.LoadGoals( userId ));
        return user;
    }

    // Load User
    public static User LoadUser(string UserId)
    {
        // Define the directory and file path
        string csvDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv");
        string path = Path.Combine(csvDirectory, "users.csv");
        
        // Check if directory exists
        if (!Directory.Exists(csvDirectory))
        {
            Console.WriteLine("CSV directory not found.");
            return null;
        }
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Users file not found.");
            return null;
        }
        
        User userFromCsv = null; 
        // Read all lines from the CSV
        var lines = File.ReadAllLines(path);
        // Iterate through each line in the file
        foreach (var line in lines)
        {
            if (line.StartsWith("Philosophy")) continue;
            string[] parts = line.Split(new string[] { "<><>" }, StringSplitOptions.None);
            if (parts[5] == UserId)
            {
                userFromCsv = User.FromCsv(line);
            }
        } 
        return userFromCsv;
    }

    public static User LoadUserByName(string Name)
    {
        // Define the directory and file path
        string csvDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "csv");
        string path = Path.Combine(csvDirectory, "users.csv");
        
        // Check if directory exists
        if (!Directory.Exists(csvDirectory))
        {
            Console.WriteLine("CSV directory not found.");
            return null;
        }
        
        if (!File.Exists(path))
        {
            Console.WriteLine("Users file not found.");
            return null;
        }
        
        User userFromCsv = null; 
        // Read all lines from the CSV
        var lines = File.ReadAllLines(path);
        // Iterate through each line in the file
        foreach (var line in lines)
        {
            if (line.StartsWith("Philosophy")) continue;
            string[] parts = line.Split(new string[] { "<><>" }, StringSplitOptions.None);
            if (parts[12] == Name)
            {
                userFromCsv = User.FromCsv(line);
            }
        } 
        return userFromCsv;
    }

    // Methods
    public void BirthdayManager()
    {
        DateTime birthday = DateTime.ParseExact(GetBirth(), "MM-dd-yyyy", null);
        DateTime today = DateTime.Today;

        // Construct this year's birthday
        DateTime thisYearsBirthday = new DateTime(today.Year, birthday.Month, birthday.Day);

        if (thisYearsBirthday == today)
        {
            System.Console.WriteLine("Happy Birthday!!!");
            _Age++;
        }
    }

    public void ListAllGoals()
    {
        GetGoals().ForEach( goal => goal.PrintGoal() );
    }

    public void ListActiveGoals()
    {
        GetActiveGoals().ForEach(goal => goal.PrintGoal());
    }

    public void ListInactiveGoals()
    {
        GetInactiveGoals().ForEach(goal => goal.PrintGoal());
    }

    public void PrintUser()
    {
        System.Console.WriteLine("==============================");
        System.Console.WriteLine($"-=|User Information");        
        System.Console.WriteLine($"  Name: {GetName()}");        
        System.Console.WriteLine($"  Age: {GetAge()}");        
        System.Console.WriteLine( _IsMale ? "  Sex: Male" : "  Sex: Female");        
        System.Console.WriteLine($"  Height: {GetHeight()}");        
        System.Console.WriteLine($"  Weight: {GetWeight()}");        
        System.Console.WriteLine( _IsImperial ? "  Measurement: Imperial" : "  Measurement: Metric");        
        System.Console.WriteLine();
        System.Console.WriteLine($"-=|Goal Statistics");        
        System.Console.WriteLine($"  Total Goals: {GetGoals().Count}");        
        System.Console.WriteLine($"  Active Goals: {GetActiveGoals().Count}");        
        System.Console.WriteLine($"  Inactive Goals: {GetInactiveGoals().Count}");        
        System.Console.WriteLine();
        System.Console.WriteLine($"-=|Philosophy Information");        
        System.Console.WriteLine($"  Philosophy: {GetPhilosophy().GetPhilosophy}");
        System.Console.WriteLine($"  Purpose: {GetPhilosophy().GetPurpose}");
        System.Console.WriteLine($"  Slogan: {GetPhilosophy().GetSlogan}");
        System.Console.WriteLine("==============================");
    }

    public void CompletePhilosophy()
    {
        GetPhilosophy().CompletePhilosophy();

        System.Console.WriteLine("Time to set up a new one!");
        System.Console.WriteLine("What is the Philosophy?");
        string getPhil = Console.ReadLine().Trim();
        System.Console.WriteLine("What is the Purpose?");
        string getPurp = Console.ReadLine().Trim();
        System.Console.WriteLine("What is the Slogan?");
        string getSlo = Console.ReadLine().Trim();
        Philosophy newPhilosophy = new Philosophy(getPhil, getPurp, getSlo);
    }

    public static int SelectGoal( List<Goal> goals)
    {
        int goalCounter = 1;
        goals.ForEach( goal =>{System.Console.WriteLine($"{goalCounter}: {goal.GetDescription()}"); goalCounter++;});
        
        System.Console.WriteLine("Input Goal Number: ");
        int userGoalSelect = int.Parse(Console.ReadLine().Trim());
        return --userGoalSelect;
    }

    public void GoalCheckIn()
    {
        int goalIdx = SelectGoal( GetGoals() );
        FitnessGoal goal = GetGoals()[goalIdx] as FitnessGoal;
        goal.CheckIn();
    }

    public void NewGoal()
    {
        System.Console.WriteLine("< A > Strength Goal");
        System.Console.WriteLine("< B > Endurance Goal");
        System.Console.WriteLine("< C > Weight-loss Goal");
        string choice = Console.ReadLine().Trim().ToUpper();


        System.Console.WriteLine("What is the goal description?");
        string desc = Console.ReadLine().Trim();
        System.Console.WriteLine("What will be your reward upon completion?");
        string rwrd = Console.ReadLine().Trim();
        string userId = GetUserId();

        switch (choice)
        {
            case "A":
                System.Console.WriteLine("What is the lift (e.g. bench, squat, etc.)?");
                string lift = Console.ReadLine().Trim();
                System.Console.WriteLine("What is your current PR?");
                string currentPr = Console.ReadLine().Trim();
                System.Console.WriteLine("What is your goal PR?");
                string goalPr = Console.ReadLine().Trim();

                GetGoals().Add(
                    new StrengthGoal( userId, desc, rwrd,
                                      lift, currentPr, goalPr));
            break;
            case "B":
                System.Console.WriteLine("What is the exercise (e.g. 100 meter, plank, etc.)?");
                string exercise = Console.ReadLine().Trim();
                System.Console.WriteLine("What is your current PR?");
                string currentBest = Console.ReadLine().Trim();
                System.Console.WriteLine("What is your goal PR?");
                string goalTime = Console.ReadLine().Trim();

                GetGoals().Add(
                    new EnduranceGoal( userId, desc, rwrd,
                                      exercise, currentBest, goalTime));
            break;
            case "C":
                System.Console.WriteLine("What is your current weight?");
                string currentWeight = Console.ReadLine().Trim();
                System.Console.WriteLine("What is your goal weight?");
                string goalWeight = Console.ReadLine().Trim();

                GetGoals().Add(
                    new WeightLossGoal( userId, desc, rwrd,
                                      currentWeight, goalWeight));
            break;
            default:
            System.Console.WriteLine("ERROR. Invalid Input.");
            return;
        }
    }
}