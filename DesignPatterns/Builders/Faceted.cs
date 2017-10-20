using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builders
{
    public class Person
    {
        public string Address, PostalCode, City;
        public string CompanyName, Posistion;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(Address)}: {Address}, {nameof(PostalCode)}: {PostalCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Posistion)}: {Posistion}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    //Facade
    public class PersonBuilder 
    {
        //reference object
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAdressBuilder Lives => new PersonAdressBuilder(person);

        public static implicit operator Person(PersonBuilder p)
        {
            return p.person;
        }
    }

    public class PersonAdressBuilder : PersonBuilder
    {
        public PersonAdressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAdressBuilder At(string address)
        {
            person.Address = address;
            return this;
        }

        public PersonAdressBuilder PostalCode(string postalcode)
        {
            person.PostalCode = postalcode;
            return this;
        }

        public PersonAdressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonBuilder AsA(string position)
        {
            person.Posistion = position;
            return this;
        }

        public PersonBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class Faceted
    {
        static void Main(string[] args)
        {
            var personBuilder = new PersonBuilder();
            Person person = personBuilder
                .Lives.At("100 Anystreet")
                .Lives.In("SomeCountry")
                .Lives.PostalCode("01234")
                .Works.At("Google")
                .Works.AsA("Engineer")
                .Works.Earning(100000);

            Console.WriteLine(person);          
        }
    }
}
