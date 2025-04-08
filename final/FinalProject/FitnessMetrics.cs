using System.ComponentModel;

class FitnessMetrics {
    public static double LbsToKgs( double lbs)
    {
        return lbs * 0.453592;
    }

    public static double InToCm( double inches)
    {
        return inches * 2.54;
    }

    public static double KgsToLbs( double kgs)
    {
        return kgs / 0.453592;
    }

    public static double CmToIn( double cm )
    {
        return cm / 2.54;
    }

    public static void CalcBMI(User user) {
        // BMI Formula: weight (kg) / (height (m) * height (m))
        // or weight (lbs) / (height (in) * height (in)) * 703
        // Need to access user.weight and user.height from the User object.
        // Convert units to kg and meters if needed.
        if(user.GetIsImperial())
        {
            double weightLbs = KgsToLbs(user.GetWeight());
            double heightIn = CmToIn(user.GetHeight());

            double BMI = weightLbs / ( heightIn * heightIn ) * 703;
            System.Console.WriteLine( $"Body Mass Index: "+BMI.ToString("F2"));
        } else 
        {
            double heightM = user.GetHeight() / 100.0; // Convert cm to meters
            double BMI = user.GetWeight() / (heightM * heightM);
            System.Console.WriteLine( $"Body Mass Index: "+BMI.ToString("F2"));
        }
    }

    public static double GetCalcBMI(User user) {
        // BMI Formula: weight (kg) / (height (m) * height (m))
        // or weight (lbs) / (height (in) * height (in)) * 703
        // Need to access user.weight and user.height from the User object.
        // Convert units to kg and meters if needed.
        double BMI;
        if(user.GetIsImperial())
        {
            double weightLbs = KgsToLbs(user.GetWeight());
            double heightIn = CmToIn(user.GetHeight());

            BMI = weightLbs / ( heightIn * heightIn ) * 703;
        } else 
        {
            double heightM = user.GetHeight() / 100.0; // Convert cm to meters
            BMI = user.GetWeight() / (heightM * heightM);
        }
        return BMI;
    }

    public static void CalcBMR(User user) {
        // BMR (Mifflin-St Jeor Equation, often considered more accurate):
        // Men: (10 × weight in kg) + (6.25 × height in cm) - (5 × age in years) + 5
        // Women: (10 × weight in kg) + (6.25 × height in cm) - (5 × age in years) - 161
        // Need to access user.weight, user.height, user.age, and user.gender from the User object.
        double weight = user.GetWeight();
        double height = user.GetHeight();
        double age = user.GetAge();
        double BMR;

        switch (user.GetIsMale())
        {
            case true:
                BMR = (10 * weight) + (6.25 * height) - (5 * age) + 5;
            break;
            case false:
                BMR = (10 * weight) + (6.25 * height) - (5 * age) - 161;
            break;
        }
        
        System.Console.WriteLine($"Basal Metabolic Rate: {BMR.ToString("F2")}");
    }

       public static double GetCalcBMR(User user) {
        // BMR (Mifflin-St Jeor Equation, often considered more accurate):
        // Men: (10 × weight in kg) + (6.25 × height in cm) - (5 × age in years) + 5
        // Women: (10 × weight in kg) + (6.25 × height in cm) - (5 × age in years) - 161
        // Need to access user.weight, user.height, user.age, and user.gender from the User object.
        double weight = user.GetWeight();
        double height = user.GetHeight();
        double age = user.GetAge();
        double BMR;

        switch (user.GetIsMale())
        {
            case true:
                BMR = (10 * weight) + (6.25 * height) - (5 * age) + 5;
            break;
            case false:
                BMR = (10 * weight) + (6.25 * height) - (5 * age) - 161;
            break;
        }

        return BMR;    
    } 

    public static void CalcTDEE(User user) {
        // TDEE = BMR * Activity Factor
        // Activity Factor:
        //   Sedentary (little or no exercise): 1.2
        //   Lightly active (light exercise/sports 1-3 days/week): 1.375
        //   Moderately active (moderate exercise/sports 3-5 days/week): 1.55
        //   Very active (hard exercise/sports 6-7 days a week): 1.725
        //   Extra active (very hard exercise/sports & physical job or 2x training): 1.9
        double BMR = FitnessMetrics.GetCalcBMR(user);

        System.Console.WriteLine("What is your weekly activity: ");
        System.Console.WriteLine("< A > Sedentary (little or no exercise): 1.2");
        System.Console.WriteLine("< B > Lightly active (light exercise/sports 1-3 days/week): 1.375");
        System.Console.WriteLine("< C > Moderately active (moderate exercise/sports 3-5 days/week): 1.55");
        System.Console.WriteLine("< D > Very active (hard exercise/sports 6-7 days a week): 1.725");
        System.Console.WriteLine("< E > Extra active (very hard exercise/sports & physical job or 2x training): 1.9");
        string getInput = Console.ReadLine().Trim().ToUpper();
        string getActivity = null;
        switch (getInput)
        {
            case "A":
                getActivity = "1.2";
            break;
            case "B":
                getActivity = "1.375";
            break;
            case "C":
                getActivity = "1.55";
            break;
            case "D":
                getActivity = "1.725";
            break;
            case "E":
                getActivity = "1.9";
            break;
            default:
            Environment.Exit(0);
            break;
        }
        double activity = double.Parse(getActivity);

        double TDEE = BMR * activity;
        System.Console.WriteLine($"TDEE: {TDEE.ToString("F2")}");
    }

    public static double GetCalcTDEE(User user) {
        // TDEE = BMR * Activity Factor
        // Activity Factor:
        //   Sedentary (little or no exercise): 1.2
        //   Lightly active (light exercise/sports 1-3 days/week): 1.375
        //   Moderately active (moderate exercise/sports 3-5 days/week): 1.55
        //   Very active (hard exercise/sports 6-7 days a week): 1.725
        //   Extra active (very hard exercise/sports & physical job or 2x training): 1.9
        double BMR = FitnessMetrics.GetCalcBMR(user);

        System.Console.WriteLine("What is your weekly activity: ");
        System.Console.WriteLine("< A > Sedentary (little or no exercise)");
        System.Console.WriteLine("< B > Lightly active (light exercise/sports 1-3 days/week)");
        System.Console.WriteLine("< C > Moderately active (moderate exercise/sports 3-5 days/week)");
        System.Console.WriteLine("< D > Very active (hard exercise/sports 6-7 days a week)");
        System.Console.WriteLine("< E > Extra active (very hard exercise/sports & physical job or 2x training)");
        string getInput = Console.ReadLine().Trim().ToUpper();
        string getActivity = null;
        switch (getInput)
        {
            case "A":
                getActivity = "1.2";
            break;
            case "B":
                getActivity = "1.375";
            break;
            case "C":
                getActivity = "1.55";
            break;
            case "D":
                getActivity = "1.725";
            break;
            case "E":
                getActivity = "1.9";
            break;
            default:
            Environment.Exit(0);
            break;
        }
        double activity = double.Parse(getActivity);
        double TDEE = BMR * activity;
        return TDEE;
    }

    public static void CalcOneRepMax() {
        // There are several formulas, here are a few common ones.
        // 1. Epley Formula: weight * (1 + (reps / 30))
        // 2. Brzycki Formula: weight / (1.0278 - (0.0278 * reps))
        //Need to access weight and reps from the user input.
        System.Console.WriteLine("How much weight did you lift?");
        double weight = double.Parse(Console.ReadLine().Trim());

        System.Console.WriteLine("How much reps did you get?");
        double reps = double.Parse(Console.ReadLine().Trim());

        double OneRep = weight * (1 + (reps / 30));
        double OneRep2 = weight / (1.0278 - (0.0278 * reps)); 

        System.Console.WriteLine($"Your one-rep-max is likely around {OneRep.ToString("F2")} or {OneRep2.ToString("F2")}.");
    }

    public static double GetCalcOneRepMax(double weight, double reps) {
        // There are several formulas, here are a few common ones.
        // 1. Epley Formula: weight * (1 + (reps / 30))
        // 2. Brzycki Formula: weight / (1.0278 - (0.0278 * reps))
        //Need to access weight and reps from the user input.
        double OneRep = weight * (1 + (reps / 30));
        return OneRep;
    }

    public static void CalcMaxBPM(User user) {
        // Max heart rate = 220 - age
        double age = user.GetAge();

        double maxHr = 220 - age;
        System.Console.WriteLine($"Max BPM: {maxHr.ToString("F2")}");
    }

    public static double GetCalcMaxBPM(User user) {
        // Max heart rate = 220 - age
        double age = user.GetAge();

        double maxHr = 220 - age;
        return maxHr;
    }

    public static void SuperReport( User user, double weight, double height )
    {
        System.Console.WriteLine("|==========Super Report==========|");
        System.Console.WriteLine($"   BMI: {GetCalcBMI( user ).ToString("F3")}");
        System.Console.WriteLine($"   BMR: {GetCalcBMR( user ).ToString("F3")}");
        System.Console.WriteLine($"   TDEE: {GetCalcTDEE( user ).ToString("F3")}");
        System.Console.WriteLine($"   One-rep-max: {GetCalcOneRepMax( weight, height ).ToString("F3")}");
        System.Console.WriteLine($"   Max BPM: {GetCalcMaxBPM( user ).ToString("F3")}");
        System.Console.WriteLine("|================================|");
    }
}