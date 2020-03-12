using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alarm
{
    class Program
    {
        public delegate void AlarmHandler(int hour);
        public delegate void TickHandler(int second, int minite, int hour);
        public class CLock
        {
            private int minite;
            private int hour;
            private int second;
            public int Minite { get; }
            public int Second { get; }
            public int Hour { get; }
            public CLock(int hour,int minite,int second)
            {
                this.minite = minite;
                this.second = second;
                this.hour = hour;
            }
            public event AlarmHandler AlarmClock;
            public event TickHandler TickClock;
            public void WorkingClock()
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);//waiting for one second
                    if (second == 59)
                    {
                        second = 0;
                        if (minite == 59)
                        {
                            minite = 0;
                            if (hour == 23) { hour = 0; AlarmClock(hour); } else hour++;
                        }
                        else minite++;
                    }
                    else second++;
                    TickClock(second, minite, hour);

                }
            }
        }
        

        static void Main(string[] args)
        {
            CLock MyClock = new CLock(23, 59, 56);
            void AlarmFunc(int hour)
            {
                Console.WriteLine($"This is {hour} o'clock now.");
            }
            void TickFunc(int second,int minite,int hour)
            {
                Console.WriteLine("Tic tok.");
            }
            MyClock.AlarmClock += AlarmFunc;
            MyClock.TickClock += TickFunc;
            MyClock.WorkingClock();
        }
    }
}
