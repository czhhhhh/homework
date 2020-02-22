using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformCalculate
{
    class MyChoice
    {
        public MyChoice(char choice)
        {
            Choice = choice;
        }
        public char Choice { get; private set; }
    }
}
