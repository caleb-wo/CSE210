public class Fraction 
{
    private int numerator;
    private int denominator;
    private double point;

    public Fraction() 
    {
        this.numerator = 1;
        this.denominator = 1;
        setPoint();
    }
    public Fraction(int numerator)
    {
        this.numerator = numerator;
        this.denominator = 1;
        setPoint();
    }
    public Fraction(int numerator ,int denominator)
    {
        this.numerator = numerator;
        this.denominator = denominator;
        setPoint();
    }

    public int getNumerator()
    {
        return this.numerator;
    }
    public int getDenominator()
    {
        return this.denominator;
    }
    public void setNumerator(int number) 
    {
        this.numerator = number;
    } 
    public void setDenominator(int number) 
        {
            this.denominator = number;
        }
    public void setPoint()
    {
        this.point = this.numerator / this.denominator;
    }
    
    public string getFractionString() 
    {
        return $"{getNumerator()}/{getDenominator()}";
    }
    public double getDecimalValue()
    {
        return this.point;
    }
    public void consoleOutput() 
    {
        Console.WriteLine("================");
        Console.WriteLine($"Fraction: {getFractionString()}");
        Console.WriteLine($"Decimal: {getDecimalValue()}");
        Console.WriteLine("================");
    }
}



