using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ShapeCreate
{
    class Program
    {
        public static Shape FactoryCreate(string shapeName)
        {
            Shape shape;
            Random num = new Random();
            Triangle tri = new Triangle(1, 3, 4);
            if(shapeName== "Rectangle")
            {
                shape = new Rectangle(num.Next(1, 10), num.Next(1, 10));
            }else if(shapeName == "Triangle")
            {
                shape = new Triangle(num.Next(1, 10), num.Next(1, 10), num.Next(1, 90));
            }else if (shapeName == "Square")
            {
                shape = new Square(num.Next(1, 10));
            }
            else
            {
                shape = new Square(num.Next(1, 10));
            }
            return shape;  
        }
        static void Main(string[] args)
        {
            Console.WriteLine("You want to create what type of shape?" +
                " ( write the name with first letter in upper case)");
            string shapeName = Console.ReadLine();
            Console.WriteLine("How many shape do you want to create? (put in an integer)");
            int numberOfShape = Int32.Parse(Console.ReadLine());
            Shape[] products = new Shape[numberOfShape];

            for (int num = 1; num <= numberOfShape; num++)
            {
                products[num-1]=FactoryCreate(shapeName);
            }
            if(shapeName!="Triangle" && shapeName!="Square" && shapeName != "Rectangle")
            {
                Console.WriteLine("This type of shape does not exsist." +
                    "We create {0} Squares for you instead.", numberOfShape);
            }
            else
            {
                Console.WriteLine("We create {0} {1} successfully.", numberOfShape, shapeName);
            }
        }
    }
}
