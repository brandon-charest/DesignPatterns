using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Principles
{
    public enum Color
    {
        Red,Green,Blue
    }

    public enum Size
    {
        Small,Medium,Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    public class ProductFilter
    {
        // below is an example of not following the open close principle, where if a new filter
        // needed to be added you would have to go back and rewrite it
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach(var p in products)
            {
                if(p.Size == size)
                {
                    //yeild return to return each element one at a time.
                    yield return p;
                }
            }
        }
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                {
                    //yeild return to return each element one at a time.
                    yield return p;
                }
            }
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Color color, Size size)
        {
            foreach(var p in products)
            {
                if(p.Color == color && p.Size == size)
                {
                    yield return p;
                }
            }
        }

    }

    // open for extention of filter but closed for modification
    // for more functionality create a new class and impliment ISpecification
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class NewFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
          foreach(var i in items)
            {
                if(spec.IsSatisfied(i))
                {
                    yield return i;
                }
            }
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {

            if (first == null)
            {
                throw new ArgumentNullException(paramName: nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(paramName: nameof(second));
            }

            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class OpenClosePrinciple
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Red, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Medium);
            var house = new Product("House", Color.Blue, Size.Large);


            Product[] products = { apple, tree, house };

            var productFilter = new ProductFilter();
            Console.WriteLine("Green products (old): ");
            foreach(var p in productFilter.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is Green");
            }

            var newFilter = new NewFilter();

            Console.WriteLine("Green products (new): ");
            foreach (var p in newFilter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is Green");
            }

            Console.WriteLine("Large blue products (new): ");
            foreach(var p in newFilter.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue), 
                    new SizeSpecification(Size.Large)
                )))
            {
                Console.WriteLine($" - {p.Name} is {p.Size} and {p.Color}");
            }

            Console.ReadLine();
        }
    }
}
