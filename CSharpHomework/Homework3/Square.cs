using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ShapeCreate
{
    public class Square:Shape
    {
        public double SideLength
        {
            set;get;
        }
        public Square(double side)
        {
            SideLength = side;
            ShapeName = "Square";
        }
        public override bool IsLegal()
        {
            return SideLength > 0;
        }
        public override double CalculateArea()
        {
            if (IsLegal())
            {
                return SideLength * SideLength;
            }
            else
                return 0;
        }
    }
}
