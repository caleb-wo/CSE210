public class Rectangle : Shape
{
     private double _length;
     private double _width;

     public Rectangle( double l, double w, string clr )
     {
        this._length = l;
        this._width = w;
        SetColor(clr);
     }

    public override double GetArea() => _length * _width;
}