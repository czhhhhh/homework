using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ShapeCreate
{
    public abstract class Shape
    {
        public string ShapeName
        {
            get;
            set;
        }
     
        abstract public bool IsLegal();
        abstract public double CalculateArea();
        
    }
}
