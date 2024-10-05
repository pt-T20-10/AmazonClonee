
using System;
using System.Linq.Expressions;
using System.Reflection;
class InvalidBirthdate : Exception
{
    public InvalidBirthdate(string msg) : base(msg) { }
}
//class TypeInit
//{
//    static int value = int.Parse("invalid"); // This will cause FormatException during type initialization
//}
class MyClass
{
    // Static field initialized with an invalid value
    static int myValue = int.Parse("invalid"); // This will cause a FormatException

    // Static constructor
    static MyClass()
    {


    }

    public static void DoSomething()
    {
        Console.WriteLine("This will not be printed because the static initialization failed.");
    }
}
class Person
{
    #region Bỉthdate

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
        }
    }
    private int _age;
    public int Age
    {
        get
        {
            return _age;
        }
    }

    private DateTime _birthdate;
    public DateTime Birthdate
    {
        get
        {
            return _birthdate;
        }

        set
        {
            if (value >= DateTime.Today)
            {
                throw
                    new InvalidBirthdate("Your birthdate can't be greater than today");
            }
            else
            {
                _birthdate = value;
                _age = Convert.ToInt32(DateTime.Today.Year - value.Year);
            }
        }
    }
    public Person(string name, DateTime dt)
    {
        _name = name;
        Birthdate = dt;
    }
    #endregion
    static void Main(string[] args)
    {
        {
            #region DivideByZeroException
            try
            {
                int a = 10;
                int b = 0;
                int result = a / b; // This will throw DivideByZeroException
            }
            catch (DivideByZeroException dzEx)
            {
                Console.WriteLine("DivideByZeroException caught: " + dzEx.Message);
            }
            #endregion

            #region NullReferenceException
            try
            {
                string str = null;
                Console.WriteLine(str.Length); // This will throw NullReferenceException
            }
            catch (NullReferenceException nrEx)
            {
                Console.WriteLine("NullReferenceException caught: " + nrEx.Message);
            }
            #endregion

            #region TypeInitializationException
            try
            {
                // Accessing a static method will trigger the static constructor
                MyClass.DoSomething(); // This will cause TypeInitializationException
            }
            catch (TypeInitializationException ex)
            {
                Console.WriteLine("TypeInitializationException caught: " + ex.Message);

            }
            #endregion

            #region OverflowException
            try
            {
                int maxVal = int.MaxValue;
                int overflowedVal = checked(maxVal + 1); // This will throw OverflowException in checked context
            }
            catch (OverflowException oEx)
            {
                Console.WriteLine("OverflowException caught: " + oEx.Message);
            }
            #endregion


            #region 50/nums
            //int dividend = 50;
            //int userInput = 0;
            //int quotient = 0;
            //Console.WriteLine("Enter a number: ");
            //try
            //{
            //    userInput = Convert.ToInt32(Console.ReadLine());
            //    quotient = dividend / userInput;
            //}
            //catch (FormatException excepE)
            //{
            //    Console.WriteLine(excepE);
            //}
            //catch (DivideByZeroException excepE)
            //{
            //    Console.WriteLine("catch block");
            //    Console.WriteLine(excepE);
            //    Console.WriteLine("");
            //}
            //finally
            //{
            //    Console.WriteLine("finally block");
            //    if (quotient != 0)
            //    {
            //        Console.WriteLine("The Integer Quotient of 50 divided by {0} is {1}", userInput, quotient);
            //    }
            //}

            #endregion

            #region Checking Date of Birth
            //try
            //{
            //    Console.WriteLine("Please enter your name: ");
            //    string name = Console.ReadLine();

            //    Console.WriteLine("Please enter your birthdate: ");
            //    Console.Write("Year: ");
            //    int year = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Month: ");
            //    int month = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Day: ");
            //    int day = Convert.ToInt32(Console.ReadLine());
            //    DateTime dt = new DateTime(year, month, day);
            //    Person p = new Person(name, dt);
            //    Console.WriteLine();
            //    Console.WriteLine("Name: " + p.Name);
            //    Console.WriteLine("Birthdate: " + p.Birthdate.ToString("dd/MM/yyyy"));
            //    Console.WriteLine("Age: " + p.Age);
            //}
            //catch (FormatException fe)
            //{
            //    Console.WriteLine(fe.Message);
            //}
            //catch (ArgumentOutOfRangeException ae)
            //{
            //    Console.WriteLine(ae.Message);
            //}
            //catch (InvalidBirthdate ibe)
            //{
            //    Console.WriteLine(ibe.Message);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            #endregion

            Console.ReadLine();
        }
    }
}
