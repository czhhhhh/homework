using System;

namespace ConsoleCaculate
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            double a, b;
            Console.WriteLine("put in the first argument");
            s = Console.ReadLine();
            //保证输入为数字
            while (!Double.TryParse(s,out a))
            {
                Console.WriteLine("put in the first argument again");
                s = Console.ReadLine();
            }
            
            Console.WriteLine("put in the second argument");
            s = Console.ReadLine();
            while (!Double.TryParse(s, out b))
            {
                Console.WriteLine("put in the second argument again");
                s = Console.ReadLine();
            }

            string ari;
            char arith;
            Console.WriteLine("put in the sign");
            ari = Console.ReadLine();
            while(!char.TryParse(ari,out arith))
            {
                Console.WriteLine("put in the sign again");
                ari = Console.ReadLine();
            }
            
            //输出结果
            if(arith=='+')
                Console.WriteLine($"The result is:{a+b}");
            else if(arith=='-')
                Console.WriteLine($"The result is:{a - b}");
            else if(arith=='/')
                Console.WriteLine($"The result is:{a / b}");
            else if(arith=='*')
                    Console.WriteLine($"The result is:{a * b}");
        }
    }
}
