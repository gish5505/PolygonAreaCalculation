using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculation
{
    public class LinearObject
    {
        public double Width { get; private set; }

        public double ValueA { get; private set; }

        public double ValueB { get; private set; }

        public double ValueC { get; private set; }

        public LinearObject(double width, double valueA, double valueB, double valueC)
        {
            Width = width;
            ValueA = valueA;
            ValueB = valueB;
            ValueC = valueC;
        }
    }      
}
