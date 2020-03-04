using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_ShapeCreate
{
    public class Triangle:Shape
    {
        

        public double Height
        {
            set;get;
        }
        public double BottomMargin
        {
            set;get;
        }
        public double Angle
        {
            set;get;
        }
        public Triangle(double height,double bottom,double angle)
        {
            Height = height;
            BottomMargin = bottom;
            Angle = angle;
            ShapeName = "Triangle";
        }
        

        public override bool IsLegal()
        {
            return Height > 0 && BottomMargin > 0 && (Angle > 0 && Angle < 180);
        }
        public override double CalculateArea()
        {
            if (IsLegal())
            {
                return Height * BottomMargin;
            }else
                return 0;
        }
    }
}
