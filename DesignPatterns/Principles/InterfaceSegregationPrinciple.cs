using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Principles
{
    public class Document
    {

    }

    // This interface works fine if you are always working with a printer that can do all three things
    public interface IComplexPrinter
    {
        void Print(Document doc);
        void Scan(Document doc);
        void Fax(Document doc);
    }

    public class MultiFunctionPrinter : IComplexPrinter
    {
        public void Fax(Document doc)
        {
            throw new NotImplementedException();
        }

        public void Print(Document doc)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document doc)
        {
            throw new NotImplementedException();
        }
    }

    public class OldPrinter : IComplexPrinter
    {
        //cant use
        public void Fax(Document doc)
        {
            throw new NotImplementedException();
        }

        public void Print(Document doc)
        {
            //Only can use print
        }

        //cant use
        public void Scan(Document doc)
        {
            throw new NotImplementedException();
        }
    }
    
    //Segregated Interfaces
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IFax
    {
        void Fax(Document d);
    }


    public interface IMultiFunctionDevice : IPrinter, IScanner, IFax
    {

    }

    //Only impliment the interfaces we need
    public class Photocopier : IPrinter, IScanner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }


    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;
        private IFax fax;

        public MultiFunctionMachine(IPrinter printer, IScanner scanner, IFax fax)
        {
            this.printer = printer;
            this.scanner = scanner;
            this.fax = fax;
        }

        //decorators
        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        }

        public void Fax(Document d)
        {
            fax.Fax(d);
        }

    }

    public class InterfaceSegregationPrinciple
    {
        static void Main(string[] args)
        {

        }
    }
}
