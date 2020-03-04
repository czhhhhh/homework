using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ShapeCreate
{
    public class Rectangle:Shape
    {
        public double Height
        {
            set;get;
        }

        public double Length
        {
            set;get;
        }
        
        
        public Rectangle(double height,double length)
        {
            this.Height = height;
            this.Length = length;
            ShapeName = "Rectangle";
        }
        public override bool IsLegal()
        {
            return Height > 0 && Length > 0;
            
        }
        public override double CalculateArea()
        {
            if (IsLegal())
                return Length * Height;
            else return 0;
        }
    }
}
