using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace EssentialCSharp
{
    public partial class Class
    {
        private class CommandLine
        {
            public string Action;
            public string Id;
            public string FirstName;
            public string LastName;
            
            public CommandLine(string[] arguments)
            {
                for (int argumentCounter = 0; argumentCounter < arguments.Length; argumentCounter++)
                {
                    switch (argumentCounter)
                    {
                        case 0:
                            Action = arguments[0].ToLower();
                            break;
                        case 1:
                            Id = arguments[1];
                            break;
                        case 2:
                            FirstName = arguments[2];
                            break;
                        case 3:
                            LastName = arguments[3];
                            break;
                    }
                }
            }
        }
    }
    
    public partial class Class
    {
        
        private readonly ITestOutputHelper _testOutputHelper;

        public Class(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            Employee.NextId = 1000000;
            
            Employee employee1 = new Employee(_testOutputHelper);
            Employee employee2 = new Employee(_testOutputHelper);
            employee2.SetName("Inigo", "Montoya");
            employee2.Save();
            IncreaseSalary(employee2);

            employee1 = DataStorage.Load("Inigo", "Montoya");
            
            _testOutputHelper.WriteLine("{0}: {1}",
                employee1.GetName(),
                employee1.Salary);

            employee2.Title = "Computer Nerd";
            employee1.Manager = employee2;
            _testOutputHelper.WriteLine(employee1.Manager.Title);

            // employee1.Id = "490";

            var patent1 = new
            {
                Title = "Bifocals",
                YearOfPublication = "1784"
            };
            
            var patent2 = new
            {
                Title = "Phonograph",
                YearOfPublication = "1877"
            };
            
            var patent3 = new
            {
                patent1.Title,
                Year = patent1.YearOfPublication
            };
            
            _testOutputHelper.WriteLine("{0} ({1})", patent1.Title, patent1.YearOfPublication);
            _testOutputHelper.WriteLine("{0} ({1})", patent2.Title, patent2.YearOfPublication);
            
            _testOutputHelper.WriteLine(patent1.ToString());
            _testOutputHelper.WriteLine(patent2.ToString());
            _testOutputHelper.WriteLine(patent3.ToString());
            
            _testOutputHelper.WriteLine("NextId = {0}", Employee.NextId);

            // CommandLine commandLine = new CommandLine();
        }
        
        private static void IncreaseSalary(Employee employee)
        {
            employee.Salary = "Enough to survive on";
        }
    }

    public partial class Person
    {
        #region Extensibility Method Definitions

        partial void OnLastNameChanging(string value);
        partial void OnFirstNameChanging(string value);

        #endregion
        
        public Guid PersonId => _PersonId;
        private Guid _PersonId;

        public string LastName
        {
            get => _LastName;
            set
            {
                if (_LastName != value)
                {
                    OnLastNameChanging(value);
                    _LastName = value;
                }
            }
        }
        private string _LastName;

        public string FirstName
        {
            get => _FirstName;
            set
            {
                if (_FirstName != value)
                {
                    OnFirstNameChanging(value);
                    _FirstName = value;
                }
            }
        }
        private string _FirstName;
    }

    partial class Person
    {
        partial void OnLastNameChanging(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("LastName");
            }

            if (value.Trim().Length == 0)
            {
                throw new ArgumentException("LastName can't be empty.");
            }
        }
    }

    public static class SimpleMath
    {
        static int Max(params int[] numbers)
        {
            if (numbers.Length == 0)
            {
                throw new ArgumentException("numbers can't be empty");
            }

            int result;
            result = numbers[0];
            foreach (int number in numbers)
            {
                if (number > result)
                {
                    result = number;
                }
            }

            return result;
        }

        static int Min(params int[] numbers)
        {
            if (numbers.Length == 0)
            {
                throw new ArgumentException("numbers can't be empty");
            }

            int result;
            result = numbers[0];
            foreach (int number in numbers)
            {
                if (number < result)
                {
                    result = number;
                }
            }
            return result;
        }
    }

    public static class DirectoryInfoExtension
    {
        public static void CopyTo(this DirectoryInfo sourceDirectory, string target,
            SearchOption option, string searchPattern)
        {
            if (target[^1] != Path.DirectorySeparatorChar)
            {
                target += Path.DirectorySeparatorChar;
            }
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            for (int i = 0; i < searchPattern.Length; i++)
            {
                foreach (string file in Directory.GetFiles(sourceDirectory.FullName,
                    searchPattern))
                {
                    File.Copy(file, target + Path.GetFileName(file)
                        , true);
                }
            }

            if (option == SearchOption.AllDirectories)
            {
                foreach (string element in Directory.GetDirectories(sourceDirectory.FullName))
                {
                    // Copy(element, target + Path.GetFileName(element),
                    //     searchPattern);
                }
            }
        }
    }

    class Employee
    {
        private readonly ITestOutputHelper _testOutputHelper;

        static Employee()
        {
            Random randomGenerator = new Random();
            NextId = randomGenerator.Next(101, 999);
        }

        public Employee()
        {
            int id = NextId;
            NextId++;
            string firstName = null;
            string lastName = null;
            Initialize(id, firstName, lastName);
        }
        
        public Employee(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            int id = NextId;
            NextId++;
            string firstName = null;
            string lastName = null;
            Initialize(id, firstName, lastName);
        }

        public Employee(string firstName, string lastName)
        {
            int id = NextId;
            NextId++;
            Initialize(id, firstName, lastName);
        }

        public Employee(int id, string firstName, string lastName) : this(firstName, lastName)
        {
            Initialize(id, firstName, lastName);
        }

        public Employee(int id)
        {
            string firstName = null;
            string lastName = null;
            Initialize(id, firstName, lastName);
        }

        public static int NextId = 42;

        private string _FirstName;
        private string _LastName;

        public string FirstName
        {
            get => _FirstName;
            set => _FirstName = value;
        }

        public string LastName
        {
            get => _LastName;
            set {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    value = value.Trim();
                    if (value == "")
                    {
                        throw new ArgumentException("LastName cannot be blank.", nameof(value));
                    }
                    else
                    {
                        _LastName = value;
                    }
                }
            }
        }

        public string Name
        {
            get => FirstName + " " + LastName;
            set
            {
                string[] names;
                names = value.Split(new char[] {' '});
                if (names.Length == 2)
                {
                    FirstName = names[0];
                    LastName = names[1];
                }
                else
                {
                    throw new ArgumentException(string.Format("Assigned value '{0}" +
                                                              " is invalid'", value));
                }
            }
        }
        
        public string Title { get; set; }
        public Employee Manager { get; set; }

        private int _Id;

        public int Id
        {
            get => _Id;
            private set => _Id = value;
        }

        public string Salary = "Not enough";
        private string Password;
        private bool IsAuthenticated;

        public void Initialize(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        
        public bool Logon(string password)
        {
            if (Password == password)
            {
                IsAuthenticated = true;
            }

            return IsAuthenticated;
        }

        public bool GetIsAuthenticated()
        {
            return IsAuthenticated;
        }

        public string GetName()
        {
            return _FirstName + " " + _LastName;
        }

        public void SetName(string FirstName, string LastName)
        {
            this._FirstName = FirstName;
            this._LastName = LastName;
            _testOutputHelper.WriteLine("Name changed to '{0}'", this.GetName());
        }

        public void Save()
        {
            DataStorage.Store(this);
        }
    }

    class DataStorage
    {
        public static void Store(Employee employee)
        {
            FileStream stream = new FileStream(employee.FirstName + employee.LastName + ".dat",
                FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            
            writer.WriteLine(employee.FirstName);
            writer.WriteLine(employee.LastName);
            writer.WriteLine(employee.Salary);
            
            writer.Close();
        }

        public static Employee Load(string firstName, string lastName)
        {
            Employee employee = new Employee();
            
            FileStream stream = new FileStream(firstName + lastName + ".dat", 
                FileMode.Open);
            
            StreamReader reader = new StreamReader(stream);
            employee.FirstName = reader.ReadLine();
            employee.LastName = reader.ReadLine();
            employee.Salary = reader.ReadLine();
            reader.Close();

            return employee;
        }
    }
}