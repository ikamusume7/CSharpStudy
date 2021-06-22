using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading;
using CountDownTimer = System.Timers.Timer;
using Timer = System.Timers.Timer;

namespace EssentialCSharp
{
    public class Function
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Function(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void MethodsDemo()
        {
            string firstName;
            string lastName;
            string fullName;
            
            _testOutputHelper.WriteLine("Hey you!");
            firstName = GetUserInput("Enter your first name: ");
            lastName = GetUserInput("Enter your last name: ");

            fullName = GetFullName(firstName, lastName);
            
            DisplayGreeting(fullName);

        }
        
        [Fact]
        public bool ReturnDemo()
        {
            string command = ObtainCommand();
            switch (command)
            {
                case "quit":
                    return false;
                default:
                    return true;
            }
        }

        [Fact]
        public void UsingAliasDemo()
        {
            CountDownTimer timer1;
            Timer timer2;
            System.Threading.Timer timer3;
        }

        [Fact]
        public void NormalParamsDemo()
        {
            string fullName;
            string driverLetter = "C:";
            string folderPath = "Data";
            string fileName = "index.html";

            fullName = Combine(driverLetter, folderPath, fileName);
            
            _testOutputHelper.WriteLine(fullName);
        }

        [Fact]
        public void RefParamsDemo()
        {
            string first = "hello";
            string second = "goodbye";
            Swap(ref first, ref second);
            _testOutputHelper.WriteLine(@"first = ""{0}"", second = ""{1}""",
                first, second);
        }

        [Fact]
        public void OutParamsDemo()
        {
            string[] args = new string[1];
            args[0] = "1ad";
            Main(args);
        }

        /// <summary>
        /// 可变长参数演示
        /// </summary>
        [Fact]
        public void VariableParamsDemo()
        {
            string fullname = Combine(Directory.GetCurrentDirectory(),
                "bin", "config", "index.htm");
            _testOutputHelper.WriteLine(fullname);

            fullname = Combine(Environment.SystemDirectory,
                "Temp", "index.html");
            _testOutputHelper.WriteLine(fullname);

            fullname = Combine(
                new string[] {
                    "C:\\", "Data", 
                    "HomeDir", "index.html"});
            _testOutputHelper.WriteLine(fullname);
        }

        /// <summary>
        /// 递归演示
        /// </summary>
        [Fact]
        public void RecursionDemo()
        {
            int totalLineCount = 0;
            string directory = Directory.GetCurrentDirectory();
            totalLineCount = DirectoryCountLines(directory);
            _testOutputHelper.WriteLine(totalLineCount.ToString());
        }

        /// <summary>
        /// 命名参数演示
        /// </summary>
        [Fact]
        public void NameParamsDemo()
        {
            DisplayGreeting(firstName: "Inigo", lastName: "Montoya");
        }

        [Fact]
        public void TryParseDemo()
        {
            string ageText = "100";
            int age;
            string firstName = "";
            if (int.TryParse(ageText, out age))
            {
                _testOutputHelper.WriteLine("Hi {0}! You are {1} months old.", firstName, 
                    age * 12);
            }
            else
            {
                _testOutputHelper.WriteLine("The age entered ,{0}, is not valid.", ageText);
            }
        }

        private void DisplayGreeting(string firstName, string middleName = default(string), 
            string lastName = default(string))
        {
            
        }

        private static int DirectoryCountLines()
        {
            return DirectoryCountLines(Directory.GetCurrentDirectory());
        }

        private static int DirectoryCountLines(string directory)
        {
            return DirectoryCountLines(directory, "*.cs");
        }

        private static int DirectoryCountLines(string directory, string extension = "*.cs")
        {
            int lineCount = 0;
            foreach (string file in Directory.GetFiles(directory, extension))
            {
                lineCount += CountLines(file);
            }

            foreach (string subdirectory in Directory.GetDirectories(directory))
            {
                lineCount += DirectoryCountLines(subdirectory, extension);
            }
            
            return lineCount;
        }

        private static int CountLines(string file)
        {
            string line;
            int lineCount = 0;
            FileStream stream = new FileStream(file, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            line = reader.ReadLine();
            while (line != null)
            {
                if (line.Trim() != "")
                {
                    lineCount++;
                }

                line = reader.ReadLine();
            }
            reader.Close();
            return lineCount;
        }

        private int Main(string[] args)
        {
            char button;

            if (args.Length == 0)
            {
                _testOutputHelper.WriteLine("ConvertToPhoneNumber.exe <phrase>");
                _testOutputHelper.WriteLine("'_' indicates no standard phone button");
                return 1;
            }

            foreach (string word in args)
            {
                foreach (char character in word)
                {
                    if (TryGetPhoneButton(character, out button))
                    {
                        _testOutputHelper.WriteLine(button.ToString());
                    }
                    else
                    {
                        _testOutputHelper.WriteLine('_'.ToString());
                    }
                }
            }
            _testOutputHelper.WriteLine("");
            return 0;
        }

        private static bool TryGetPhoneButton(char charactor, out char button)
        {
            bool success = true;
            switch (char.ToLower(charactor))
            {
                case '1':
                    button = '1';
                    break;
                case '2': case 'a' : case 'b' : case 'c':
                    button = '2';
                    break;
                case '-':
                    button = '-';
                    break;
                default:
                    button = '_';
                    success = false;
                    break;
            }

            return success;
        }

        private void Swap(ref string x, ref string y)
        {
            string temp = x;
            x = y;
            y = temp;
        }

        private string Combine(string driverLetter, string folderPath, string fileName)
        {
            string path = string.Format("{1}{0}{2}{0}{3}", Path.DirectorySeparatorChar,
                driverLetter, folderPath, fileName);
            return path;
        }
        
        private string Combine(params string[] paths)
        {
            string result = string.Empty;
            foreach (string path in paths)
            {
                result = Path.Combine(result, path);
            }
            return result;
        }


        private string ObtainCommand()
        {
            return "";
        }

        private static string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private static string GetFullName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        private static void DisplayGreeting(string name)
        {
            Console.WriteLine("Your full name is {0}.", name);
            return;
        }
    }
}