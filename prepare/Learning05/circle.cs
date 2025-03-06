public class Circle : Shape
{
     private double _radius;

     public Circle( double r, string clr )
     {
        this._radius = r;
        SetColor(clr);
     }

    public override double GetArea() => Math.Sqrt(Math.PI * _radius);
}