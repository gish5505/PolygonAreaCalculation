using PolyBool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonAreaCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputData = new InputData();

            Polygon convexPolygon;
            convexPolygon = GetPolygon(inputData.PairsOfCoordinatesValues);

            var minXPoint = new Point(inputData.GetMinX(), 0);
            var maxXPoint = new Point(inputData.GetMaxX(), 0);

            var minYPoint = new Point(0, inputData.GetMinY());
            var maxYPoint = new Point(0, inputData.GetMaxY());
            

            List<Polygon> linearPolygonsList = new List<Polygon>();

            List<Polygon> intersectedPolygonsList = new List<Polygon>();

            List<Polygon> intersectedLinearPolygonList = new List<Polygon>();

            double sumOfAllSquares = 0;

            double sumOfLinearPolygons = 0;

            var squareCalculator = new SquareCalculator();

            foreach (var linearObject in inputData.LinearObjects)
            {
                var polygonFromLine = new PolygonFromLine();

                linearPolygonsList.Add(polygonFromLine.CreatePolygonFromLine(linearObject, minXPoint, maxXPoint, minYPoint, maxYPoint));
            }

            foreach (var linearPolygon in linearPolygonsList)
            {
                var intersectedPolybool = new PolyBool.PolyBool();
                                
                intersectedPolygonsList.Add(intersectedPolybool.Intersect(convexPolygon,linearPolygon));                                
            }

            if (intersectedPolygonsList.Count > 1)
            {
                for (int i = 0; i < intersectedPolygonsList.Count - 1; i++)
                {
                    for (int k = i + 1; k < intersectedPolygonsList.Count; k++)
                    {
                        var intersectedPoybool = new PolyBool.PolyBool();
                        var intersectedPolygon = intersectedPoybool.Intersect(intersectedPolygonsList[i], intersectedPolygonsList[k]);
                        intersectedLinearPolygonList.Add(intersectedPolygon);
                    }
                }
            }

            foreach (var intersectedPolygon in intersectedPolygonsList)
            {
                var points = GetPoints(intersectedPolygon);
                sumOfAllSquares += squareCalculator.GetSquare(points.ToList());
            }

            foreach (var intersectedLinearPolygon in intersectedLinearPolygonList)
            {
                var points = GetPoints(intersectedLinearPolygon);
                if (points.Length > 1)
                {
                    sumOfLinearPolygons += squareCalculator.GetSquare(points.ToList());
                }                
            }

            var result = sumOfAllSquares - sumOfLinearPolygons;

            Console.WriteLine($"Площадь: {result}");
            Console.ReadLine();
        }
            

        private static Polygon GetPolygon(List<Point> convexPolygonVertices)
        {
            var polygonVertices = convexPolygonVertices.ToArray();

            var region = new Region(polygonVertices);

            var regionArray = new Region[1] { region };

            var rectangle = new Polygon(regionArray, false);

            return rectangle;
        }

        private static Point[] GetPoints(Polygon polygon)
        {
            var region = polygon.Regions;
            if (!(region?.Length != 0))
            {
                var falsePoint = new Point(0, 0);
                var falsePointArray = new Point[1] { falsePoint };                
                region = new Region[1] { new Region(falsePointArray) };
            }
            var points = region[0].Points;
            
            return points;
        }

        
    }
}
