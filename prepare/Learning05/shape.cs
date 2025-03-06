public abstract class Shape
{
    private string _color;

    public string GetColor()
    {
        return this._color;
    }
    public void SetColor(string clr)
    {
        this._color = clr;
    }
    public abstract double GetArea();

}