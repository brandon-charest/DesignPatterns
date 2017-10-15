using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Principles
{
    public class Rectangle
    {
        public virtual int width { get; set; }
        public virtual int height { get; set; }

        public Rectangle() { }

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return $"{nameof(width)}: {width}, {nameof(height)}: {height}";
        }
    }

    public class Square : Rectangle
    {
        public override int width
        {
           set
            {
                base.width = base.height = value;
            }
        }

        public override int height
        {  
            set
            {
                base.height = base.width = value; 
            }
        }
    }
    public class LiskovSubstitutionPrinciple
    {

        static public int Area(Rectangle r) => r.width * r.height;

        static void Main(string[] args)
        {
            Rectangle rec = new Rectangle(2,3);

            Console.WriteLine($"{rec} has an area of {Area(rec)}");

            Square square = new Square();

            square.width = 2;
            Console.WriteLine($"{square} has an area of {Area(square)}");
            Console.ReadLine();
        }
    }
}
