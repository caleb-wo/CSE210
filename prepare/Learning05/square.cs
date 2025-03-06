public class Square : Shape
{
     private double _side;

     public Square( double s, string clr )
     {
        this._side = s;
        SetColor(clr);
     }

    public override double GetArea() => _side * _side;
}