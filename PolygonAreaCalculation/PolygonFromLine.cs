using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyBool;

namespace PolygonAreaCalculation
{
    public class PolygonFromLine
    {
        private Point _maxLeftEndPoint;

        private Point _maxRightEndPoint;

        
        public Polygon CreatePolygonFromLine(LinearObject linearObject,Point minXPoint, Point maxXPoint, Point minYPoint, Point maxYPoint)
        {
            double angleCoefficient = -linearObject.ValueA / linearObject.ValueB;

            if (angleCoefficient >= 0)
            {
                _maxLeftEndPoint = new Point(minXPoint.X, minYPoint.Y);
                _maxRightEndPoint = new Point(maxXPoint.X, maxYPoint.Y);
            }
            else
            {
                _maxLeftEndPoint = new Point(minXPoint.X, maxYPoint.Y);
                _maxRightEndPoint = new Point(maxXPoint.X, minYPoint.Y);
            }

            var primaryEndPoints = CalculatePrimaryEndPoint(linearObject.ValueA, linearObject.ValueB, linearObject.ValueC);

            var secondaryEndPoints = CalculateSecondaryEndPoints(primaryEndPoints, angleCoefficient, linearObject.Width);

            var region = new Region(secondaryEndPoints);

            var regionArray = new Region[1] { region };

            var rectangle = new Polygon(regionArray, false);

            return rectangle;
        }


        
        private Point[] CalculatePrimaryEndPoint(double a, double b, double c)
        {
            var points = new Point[2];

            if (b != 0)
            {
                var primaryLeftPoint = new Point(_maxLeftEndPoint.X, ((-c - a * _maxLeftEndPoint.X) / b));
                var primaryRightPoint = new Point(_maxRightEndPoint.X, ((-c - a * _maxRightEndPoint.X) / b));

                points[0] = primaryLeftPoint;
                points[1] = primaryRightPoint;
                 
            }
            if (b == 0)
            {
                var primaryLeftPoint = new Point(-c / a, _maxLeftEndPoint.Y);
                var primaryRightPoint = new Point(-c / a, _maxRightEndPoint.Y);

                points[0] = primaryLeftPoint;
                points[1] = primaryRightPoint;
            }
            return points;
        }

        private Point[] CalculateSecondaryEndPoints(Point[] primaryPoints, double angleCoefficient, double width)
        {            
            var secondaryEndPoints = new Point[4];

            var secondaryLeftAboveYPoint = new Point(0, 0);
            var secondaryLeftBelowYPoint = new Point(0, 0);
            var secondaryRightAboveYPoint = new Point(0, 0);
            var secondaryRightBelowYPoint = new Point(0, 0);

            var angleGrad = Math.Atan(angleCoefficient);

            switch (angleCoefficient)
            {
                case var _ when angleCoefficient > 0 && !double.IsInfinity(angleCoefficient):
                    {
                        secondaryLeftAboveYPoint = new Point(primaryPoints[0].X - (width / 2) * Math.Sin(angleGrad), primaryPoints[0].Y + (width / 2) * Math.Cos(angleGrad));
                        secondaryLeftBelowYPoint = new Point(primaryPoints[0].X + (width / 2) * Math.Sin(angleGrad), primaryPoints[0].Y - (width / 2) * Math.Cos(angleGrad));

                        secondaryRightAboveYPoint = new Point(primaryPoints[1].X - (width / 2) * Math.Sin(angleGrad), primaryPoints[1].Y + (width / 2) * Math.Cos(angleGrad));
                        secondaryRightBelowYPoint = new Point(primaryPoints[1].X + (width / 2) * Math.Sin(angleGrad), primaryPoints[1].Y - (width / 2) * Math.Cos(angleGrad));

                        break;
                    }
                case var _ when angleCoefficient < 0 && !double.IsInfinity(angleCoefficient):
                    {
                        secondaryLeftAboveYPoint = new Point(primaryPoints[0].X + (width / 2) * Math.Sin(angleGrad), primaryPoints[0].Y + (width / 2) * Math.Cos(angleGrad));
                        secondaryLeftBelowYPoint = new Point(primaryPoints[0].X - (width / 2) * Math.Sin(angleGrad), primaryPoints[0].Y - (width / 2) * Math.Cos(angleGrad));

                        secondaryRightAboveYPoint = new Point(primaryPoints[1].X + (width / 2) * Math.Sin(angleGrad), primaryPoints[1].Y + (width / 2) * Math.Cos(angleGrad));
                        secondaryRightBelowYPoint = new Point(primaryPoints[1].X - (width / 2) * Math.Sin(angleGrad), primaryPoints[1].Y - (width / 2) * Math.Cos(angleGrad));
                        
                        break;
                    }
                case var _ when (double.IsInfinity(angleCoefficient)):
                    {
                        secondaryLeftAboveYPoint = new Point(primaryPoints[0].X - (width / 2), primaryPoints[0].Y);
                        secondaryLeftBelowYPoint = new Point(primaryPoints[0].X + (width / 2), primaryPoints[0].Y);

                        secondaryRightAboveYPoint = new Point(secondaryLeftAboveYPoint.X, primaryPoints[1].Y);
                        secondaryRightBelowYPoint = new Point(secondaryLeftBelowYPoint.X, primaryPoints[1].Y);
                        
                        break;
                    }
                case var _ when (angleCoefficient == 0):
                    {
                        secondaryLeftAboveYPoint = new Point(primaryPoints[0].X, primaryPoints[0].Y + width / 2);
                        secondaryLeftBelowYPoint = new Point(primaryPoints[0].X, primaryPoints[0].Y - width / 2);

                        secondaryRightAboveYPoint = new Point(primaryPoints[1].X, primaryPoints[0].Y + width / 2);
                        secondaryRightBelowYPoint = new Point(primaryPoints[1].X, primaryPoints[0].Y - width / 2);
                        
                        break;
                    }
            }

            secondaryEndPoints[0] = secondaryLeftBelowYPoint;
            secondaryEndPoints[1] = secondaryLeftAboveYPoint;
            secondaryEndPoints[2] = secondaryRightAboveYPoint;
            secondaryEndPoints[3] = secondaryRightBelowYPoint;

            return secondaryEndPoints;
        }

        
    }
}
