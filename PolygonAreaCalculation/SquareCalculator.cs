using PolyBool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculation
{
    public class SquareCalculator
    {
        private List<Point> _currentPolygon;

        public double GetSquare(List<Point> pointsOfVertices)
        {            
            _currentPolygon = pointsOfVertices;
            
            _currentPolygon.Add(_currentPolygon[0]);

            var square = Math.Abs(_currentPolygon.Take(_currentPolygon.Count - 1)
                .Select((p, i) => (_currentPolygon[i + 1].X - p.X) * (_currentPolygon[i + 1].Y + p.Y))
                .Sum() / 2);           

            return square;
        }
    }
}
