using PolyBool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculation
{
    public class InputData 
    {
        public int NumberPairsOfCoordinates { get; set; } = 4;

        public List<Point> PairsOfCoordinatesValues { get; set; } = new List<Point> { new Point(0, 0), new Point(0, 10), new Point(10, 10), new Point(10, 0) };

        public int SetsOfLinearObjects { get; set; } = 4;

        public List<LinearObject> LinearObjects { get; set; } = new List<LinearObject>() { new LinearObject(1, 0, 1, -5), new LinearObject(1, 1, 0, -5), new LinearObject(1, 1, -1, 0)}; 


        public double GetMaxX()
        {
            var maxX = PairsOfCoordinatesValues
                .Select(p => p.X)
                .Max();

            return maxX;
        }

        public double GetMinX()
        {
            var minX = PairsOfCoordinatesValues
                .Select(p => p.X)
                .Min();

            return minX;
        }

        public double GetMaxY()
        {
            var maxY = PairsOfCoordinatesValues
                .Select(p => p.Y)
                .Max();

            return maxY;
        }

        public double GetMinY()
        {
            var minY = PairsOfCoordinatesValues
                .Select(p => p.Y)
                .Min();

            return minY;
        }

    }
}
