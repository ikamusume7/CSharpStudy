using System;
using Xunit;
using Xunit.Abstractions;

namespace EssentialCSharp.Interface
{
    public class Interface
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Interface(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            Contact[] contacts = new Contact[1];
            contacts[0] = new Contact(
                "Dick", "Traci",
                "123 Main St., Spokane, WA   99037", 
                "123-123-1234");
            
            contacts.List(Contact.Headers);
            
            _testOutputHelper.WriteLine("");

            Publication[] publications = new Publication[1]
            {
                new Publication("Celebration of Discipline",
                    "Richard Foster", 1978)
            };
            
            publications.List(Publication.Headers);
        }
    }

    interface IDistributedSettingsProvider : ISettingsProvider
    {
        string GetSetting(string machineName, string name, string defaultValue);

        void Setting(string machineName, string name, string value);
    }

    interface IPerson
    {
        string FirstName
        {
            get;
            set;
        }

        string LastName
        {
            get;
            set;
        }
    }

    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    interface IReadableSettingsProvider
    {
        string GetSetting(string name, string defaultValue);
    }

    interface IWriteableSettingsProvider
    {
        void SetSetting(string name, string value);
    }

    interface ISettingsProvider : IReadableSettingsProvider, IWriteableSettingsProvider
    {
        
    }

    class FileSettingsProvider : ISettingsProvider, IReadableSettingsProvider
    {
        #region ISettingsProvider Members
        public void SetSetting(string name, string value)
        {
            
        }
        #endregion

        #region IReadableSettingsProvider Members
        public string GetSetting(string name, string defaultValue)
        {
            return null;
        }
        #endregion
    }

    static class ConsoleListControl
    {
        public static void List(this IListable[] itmes, string[] headers)
        {
            int[] columnWidths = DisplayHeaders(headers);

            for (int count = 0; count < itmes.Length; count++)
            {
                string[] values = itmes[count].ColumnValues;
                DisplayItemRow(columnWidths, values);
            }
        }

        private static int[] DisplayHeaders(string[] headers)
        {
            return new int[1];
        }

        private static void DisplayItemRow(int[] ColumnWidths, string[] values)
        {
            
        }
    }

    interface IFileCompression
    {
        void Compress(string targetFileName, string[] fileList);
        void UnCompress(string compressFileName, string expandDirectoryName);
    }

    interface IListable
    {
        string[] ColumnValues
        {
            get;
        }
    }

    public abstract class PdaItem
    {
        public virtual string Name { get; set; }
        
        public PdaItem(string name)
        {
            Name = name;
        }
    }

    class Contact : PdaItem, IListable, IComparable, IPerson
    {
        
        private Person _Person;
        private Person Person
        {
            get => _Person;
            set => _Person = value;
        }

        public string FirstName
        {
            get => _Person.FirstName;
            set => _Person.FirstName = value;
        }

        public string LastName
        {
            get => _Person.LastName;
            set => _Person.LastName = value;
        }
        public string Address { get; set; }
        public string Phone { get; set; }
        
        public Contact(string firstName, string lastName,
            string address, string phone) : base(null)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Phone = phone;
        }

        #region IComparable Members
        
        public int CompareTo(object? obj)
        {
            int result;
            Contact contact = obj as Contact;

            if (contact == null)
            {
                result = 1;
            }
            else if (obj != typeof(Contact))
            {
                throw new ArgumentException("obj is not a Contact");
            }
            else if (ReferenceEquals(this, obj))
            {
                result = 0;
            }
            else
            {
                result = String.Compare(LastName, contact.LastName, StringComparison.Ordinal);
                if (result == 0)
                {
                    result = String.Compare(FirstName, contact.FirstName, StringComparison.Ordinal);
                }
            }

            return result;
        }

        #endregion

        #region IListable Members

        string[] IListable.ColumnValues
        {
            get
            {
                return new string[]
                {
                    FirstName,
                    LastName,
                    Phone,
                    Address
                };
            }
        }

        #endregion
        
        public string[] ColumnValues
        {
            get
            {
                return new string[]
                {
                    FirstName,
                    LastName,
                    Phone,
                    Address
                };
            }
        }

        public static string[] Headers
        {
            get
            {
                return new string[]
                {
                    "First Name", "Last Name      ",
                    "Phone       ",
                    "Address                         "
                };
            }
        }
    }

    class Publication : IListable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        
        public Publication(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public string[] ColumnValues
        {
            get
            {
                return new string[]
                {
                    Title,
                    Author,
                    Year.ToString()
                };
            }
        }

        public static string[] Headers
        {
            get
            {
                return new string[]
                {
                    "Title                                    ",
                    "Author                 ",
                    "Year"
                };
            }
        }
    }
}