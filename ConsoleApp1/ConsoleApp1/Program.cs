// See https://aka.ms/new-console-template for more information
using System;
using System.Security.AccessControl;
class Employee
{
    private string name;
    private int age;
    
    public Employee() 
    {
        name = "Mark";
        age = 25;
    }
    public Employee(string name, int age)// Parameterzied Constructor
    {
        this.name = name;
        this.age = age;
    }
    public void ShowData()
    {
        Console.WriteLine("Name = " + name);
        Console.WriteLine("Age = " + age);  
    }
    static void Main(string[] args)
    {
        /*Employee objEmpOne = new Employee(); 
        Employee objEmpTwo = new Employee("Allen", 30);
        objEmpOne.ShowData();
        Console.WriteLine();
        objEmpTwo.ShowData();
        Maths objMaths = new Maths();
        objMaths.DoOverLoad();
        Vehicle objVehicle = new Vehicle();
        objVehicle.strType = "Car";
        objVehicle.strColor = "Red";
        objVehicle.dlbSpeed = 100.2;
        objVehicle.strBrand = "BMW";
        Car objCar = new Car(objVehicle);
        objCar.Run();
        objCar.Display();
        Blue b = new Blue();
        b.Fill("Blue");

        Green g = new Green();
        g.Fill("Green");
        AppWindow win = new AppWindow(-110, -0);
        win.CreateWindow();
        win = new ListBox(3, 4, "This is a list box");
        win.CreateWindow();
        win = new Button(5, 6);
        win.CreateWindow();
        Accounts objacc = new Accounts();
        Console.WriteLine("Accounts Department");
        objacc.Salary();
        Console.WriteLine();
        HR objhr = new HR();
        Console.WriteLine("HR Department");
        objhr.Salary*/
        Console.ReadLine();
    }
}
