class Maths
{
    public void DoOverLoad()
    {
        int intX = 3;
        double dblY = 4.2;
        Console.WriteLine("Square of int value is: " + Square(intX) + "\n" + "Square of double value is : " + Square(dblY));
    }
    public int Square(int intY) => intY * intY;
    public double Square(double dblY) => dblY * dblY;

}

