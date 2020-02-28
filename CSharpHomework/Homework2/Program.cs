using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoSomething
{
    class Program
    {
        static void Main(string[] args)
        {
            //figure out the prime factors of one integer
            Console.WriteLine("Test the first functon:");
            Console.WriteLine("put in an integer radomly");

            uint test;
            while(!uint.TryParse(Console.ReadLine(),out test))
            {
                Console.WriteLine("The integer is invalid.Put in an integer again.");
            }
            //find out all the prime factors
            for(uint i = 2; i <= test; i++)
            {
                if (test % i == 0)
                {
                    test /= i;
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("All the prime factors have been found.");

            //figure out the array problems
            Console.WriteLine("Test the second functon:");
            Console.WriteLine("put in the length of an array");
            uint length;
            while (!uint.TryParse(Console.ReadLine(), out length))
            {
                Console.WriteLine("The length is invalid.Put in an number again.");
            }
            int[] TestArray = new int[length];
            
            Console.WriteLine("put in an array of integers");
            for(int i = 0; i < length; i++)
            {
                while (!int.TryParse(Console.ReadLine(), out TestArray[i]))
                {
                    Console.WriteLine("The integer is invalid.Put in an integer again.");
                }
            }

            int maxElem = TestArray[0];
            int minElem = TestArray[0];
            int sum = 0;
            for(int i = 0; i < length;i++ )
            {
                if (maxElem < TestArray[i])
                {
                    maxElem = TestArray[i];
                }
               
                if (minElem > TestArray[i])
                {
                    minElem = TestArray[i];
                }
                sum += TestArray[i];
            }
            Console.WriteLine("The maximum number is{0}", maxElem);
            Console.WriteLine("The minimum number is{0}", minElem);
            Console.WriteLine("The sum of this array is{0}", sum);
            Console.WriteLine("The average of this array is{0}", (double)sum/length);

            //figure out the third issue
            Console.WriteLine("Test the third function:");
            bool[] isPrime = new bool[101];
            for(int i = 0; i < 100; i++)
            {
                isPrime[i] = true;
            }
            for(int i = 2; i * i <= 100; i++)
            {
                for(int j = 2 * i; j <= 100; )
                {
                    if (j % i == 0)
                    {
                        isPrime[j] = false;
                    }
                    j = j + i;
                }
            }
            for(int i = 2; i < 100; i++)
            {
                if (isPrime[i] == true)
                {
                    Console.WriteLine(i);
                }
            }

        }
    }
}
