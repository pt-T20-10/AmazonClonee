using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppWindow
{
    //Protected members
    protected int top;
    protected int left;
    
    // contructor takes two integers to fix location on the console
    public AppWindow(int top, int left)
    {
        this.top = top;
        this.left = left;
    }
    //simulates drawing the AppWindow
    public virtual void CreateWindow()
    {
        Console.WriteLine("Window: drawing Window at {0}, {1}", top, left);

    }
}
//ListBox derives from AppWindow
public class ListBox  : AppWindow
{
    private string listBoxContents;
    //constructor adds a parameter and also call base constructor
    public ListBox(int top, int left, string contents) : base(top, left)
    {
        listBoxContents = contents;
    }
    //Overriding CreateWindow
    public override void CreateWindow()
    {
        base.CreateWindow(); // invoking base method
        Console.WriteLine($"Writing string to the Listbox: {listBoxContents}");
    }

}
//Button derives from AppWindow
public class Button : AppWindow
{
    public Button(int top, int left) : base(top, left) { }
    //Overriding CreateWindow
    public override void CreateWindow()
    {
       Console.WriteLine("Drawing a button at {0}, {1}\n",top,left);
    }
}

public interface Calculation
{
    void Salary();
}
public class Accounts : Calculation
{
    private int basic = 6000;
    public void Salary()
    {
        Console.WriteLine("Salary(basic * 5) = "+ basic * 5);
    }
}
public class HR : Calculation
{
    private int basic = 4000;
    public void Salary()
    {
        Console.WriteLine("Salary (basic * 2) = " + basic * 2);
    }
}

//public interface IStudent
//{
//    void Register();
//    void PostCourseWork(string work);
//}
//public class PartTimeStudent : IStudent
//{
//    private string LibCardNumber;
//    private int year;
//    public PartTimeStudent(string LibCardNumber, int year)
//    {
//        this.LibCardNumber = LibCardNumber;
//        this.year = year;
//    }

//    public PartTimeStudent()
//    {
//    }

//    public void  Register()
//    {
//        Console.WriteLine("The Student has been Register");
//        Console.WriteLine($"Library card number : {LibCardNumber}");
//        Console.WriteLine($"Year: {year}");
//    }
//    public void PostCourseWork(int work)
//    {
//        Console.WriteLine(work);
//    }

//}
