using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory
{
    public class Point
    {
        public class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }

        #region Factory Method Example
        // Factory Method design pattern
        /*
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
        */
        #endregion
        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        //proptery
        public static Point Origin => new Point(0, 0);
        //field
        public static Point Origin2 = new Point(0, 0);
    }

    class FactoryPattern
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            var origin = Point.Origin;
        }
        
    }
}
