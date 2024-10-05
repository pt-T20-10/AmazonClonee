using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Color
{
    public abstract void Fill(string strColor);
}
class Blue : Color
{
    public override void Fill(string strColor)
    {
        Console.WriteLine("Fill me up with " + strColor);
    }

}
class Green : Color
{
    public override void Fill(string strColor)
    {
        Console.WriteLine("Fill me up with " + strColor);
    }
}
