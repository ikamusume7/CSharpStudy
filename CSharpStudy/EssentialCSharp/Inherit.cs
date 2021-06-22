using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace EssentialCSharp
{
    public class Inherit
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Inherit(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            Contact contact = new Contact();
            contact.Name = "Inigo Montoya";
            // contact._Name = "Inigo Montoya";
            PdaItem item = contact;
            item.Name = "Inigo Montoya";
            
            _testOutputHelper.WriteLine("{0} {1}", contact.FirstName, contact.LastName);

            SuperSubDerivedClass superSubDerivedClass = new SuperSubDerivedClass();
            
            SubDerivedClass subDerivedClass = superSubDerivedClass;
            DerivedClass derivedClass = superSubDerivedClass;
            BaseClass baseClass = superSubDerivedClass;
            
            superSubDerivedClass.DisplayName();
            subDerivedClass.DisplayName();
            derivedClass.DisplayName();
            baseClass.DisplayName();
        }
        
        public class BaseClass
        {
            public void DisplayName()
            {
                Console.WriteLine("BaseClass");
            }
        }
        
        public class DerivedClass : BaseClass
        {
            public virtual void DisplayName()
            {
                Console.WriteLine("DerivedClass");
            }
        }
        
        public class SubDerivedClass : DerivedClass
        {
            public override void DisplayName()
            {
                Console.WriteLine("SubDerivedClass");
            }
        }
        
        public class SuperSubDerivedClass : SubDerivedClass
        {
            public new void DisplayName()
            {
                Console.WriteLine("SuperSubDerivedClass");
            }
        }
        
    }

    public class Address
    {
        public string StreetAddress;
        public string City;
        public string State;
        public string Zip;

        public override string ToString()
        {
            return string.Format("{0}" + Environment.NewLine + "{1}, {2}, {3}",
                StreetAddress, City, State, Zip);
        }
    }

    public class InternationalAddress : Address
    {
        public string Country;

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + Country;
        }
    }

    public class Controller
    {
        public void Start()
        {
            
        }

        private void Run()
        {
            Start();
            InternalRun();
            Stop();
        }

        protected virtual void InternalRun()
        {
            
        }

        public void Stop()
        {
            
        }
    }

    public sealed class CommandLineParser
    {
        
    }

    // public sealed class DerivedCommandLineParser : CommandLineParser
    // {
    //     
    // }

    class GPSCoordinates
    {
        // public static implicit operator UTMCoordinates(GPSCoordinates coordinates)
        // {
        //     
        // }
        // public static explicit operator UTMCoordinates(GPSCoordinates coordinates)
        // {
        //     
        // }
    }
    
    public class PdaItem : Object
    {
        public virtual string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        private string _Name;

        protected Guid ObjectKey
        {
            get => _ObjectKey;
            set => _ObjectKey = value;
        }
        private Guid _ObjectKey;
    }

    public class Appointment : PdaItem
    {
        
    }
    
    public class Contact : PdaItem
    {
        private Person InternalPerson { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public override string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {
                string[] names = value.Split(" ");
                FirstName = names[0];
                LastName = names[1];
            }
        }

        public string FirstName
        {
            get => InternalPerson.FirstName;
            set => InternalPerson.FirstName = value;
        }

        public string LastName
        {
            get => InternalPerson.LastName;
            set => InternalPerson.LastName = value;
        }

        void Save()
        {
            FileStream stream = File.OpenWrite(ObjectKey + ".dat");
        }

        void Load(PdaItem pdaItem)
        {
            Contact contact = pdaItem as Contact;
            if (contact != null)
            {
                // contact.ObjectKey = ...;
            }
        }
    }

    public class Customer : Contact
    {
        
    }
}