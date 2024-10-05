// See https://aka.ms/new-console-template for more information
using System;
using System.Reflection;
//using Customer;
//using Order;

//namespace Customer
//{
//    class Cust_details
//    {
//        public string cusName;
//        public void getName()
//        {
//            Console.WriteLine("Enter your name:");
//            cusName = Console.ReadLine();
//        }
//    }
//}
//namespace Order
//{
//    class GroceryItems
//    {
//        public void Grocery_ordered()
//        {
//            Cust_details customer1 = new Cust_details();
//            customer1.getName();
//            Console.WriteLine($"Hello {customer1.cusName}");
//            Console.WriteLine($"You have ordered grocery items");
//        }
//    }

//    class BakeryItems
//    {
//        public void Bakery_ordered()
//        {
//            Cust_details customer2 = new Cust_details();
//            customer2.getName();
//            Console.WriteLine($"Hello {customer2.cusName}");
//            Console.WriteLine($"You have ordered bakery items");
//        }
//    }
//}
class Test
{
    static void Main(string[] args)
    {
        int[] a = { 6, 7, 8, 9, 10, 11, 12, };
        Console.WriteLine("The value of array: "); 
        foreach (int item in a)
        {
            Console.WriteLine(item + " ");    
        }    
            
        //groceryitems groceryitems = new groceryitems();
        //bakeryitems bakeryitems = new bakeryitems();

        //groceryitems.grocery_ordered();
        //bakeryitems.bakery_ordered();
        //console.readline();
    }
}
