// See https://aka.ms/new-console-template for more information
using System;
class Vehicle
{
    public string strType;
    public string strColor;
    public double dlbSpeed;
    public string strBrand;

    public void Run()
    {
        Console.WriteLine(strType + "  : I am running");
    }
    public void Display()
    {
        Console.WriteLine("Type    : " + strType);
        Console.WriteLine("Color   :  " + strColor);
        Console.WriteLine("Speed   :  " + dlbSpeed);
        Console.WriteLine("Brand   :  " + strBrand);
    }
}
class Car : Vehicle
{
    public Car(Vehicle objVehicle)
    {
        strType = objVehicle.strType;
        strColor = objVehicle.strColor;
        dlbSpeed = objVehicle.dlbSpeed;
        strBrand = objVehicle.strBrand;
    }
    public new void Run()
    {
        Console.WriteLine("The CAR is running");
    }
}

   