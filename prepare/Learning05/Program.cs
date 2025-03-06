using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>{
             new Square( 10, "Red" )
            ,new Rectangle( 5, 100, "Green" )
            ,new Circle( 2, "Blue" )
        };

        shapes.ForEach(shape => 
        System.Console.WriteLine($"Color: "+shape.GetColor()
                                +"\nArea: "+shape.GetArea()
                                +"\n"));
    }
}