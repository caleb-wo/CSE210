using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Fraction f2 = new Fraction(1);
        Fraction f3 = new Fraction(5);
        Fraction f4 = new Fraction(3 ,4);
        Fraction f5 = new Fraction(1 ,3);

        f1.consoleOutput();
        f2.consoleOutput();
        f3.consoleOutput();
        f4.consoleOutput();
        f5.consoleOutput();
    }

}
    