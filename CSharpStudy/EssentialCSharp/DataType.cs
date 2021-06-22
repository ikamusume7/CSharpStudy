using System;
using System.Diagnostics;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace CSharpStudy
{
    public class DataType
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DataType(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void BasicDemo()
        {
            
        }

        [Fact]
        public void RoundTripDemo()
        {
            const double number = 1.618033988749895;
            double result;
            string text;
            text = $"{number}";
            result = double.Parse(text);
            _testOutputHelper.WriteLine("{0}: result != number", result != number);
            text = $"{number:R}";
            result = double.Parse(text);
            _testOutputHelper.WriteLine("{0}: result == number", result == number);
        }

        [Fact]
        public void StringDemo()
        {
            _testOutputHelper.WriteLine("\"Truly, you have a dizzying intellect.\"");
            _testOutputHelper.WriteLine("\n\"Wait 'til I get going!\"\n");
            
            _testOutputHelper.WriteLine(@"begin
                    /\
                   /  \
                  /    \
                 /      \
                /________\
            end");

            string text, firstName = "", lastName = "";
            text = $"Your full name is {firstName} {lastName}.";
            _testOutputHelper.WriteLine(text);

            text = string.Concat(firstName, lastName);
            _testOutputHelper.WriteLine(text);

            string option = "/Help";

            int result = string.Compare(option, "/help", StringComparison.Ordinal);
            _testOutputHelper.WriteLine(result.ToString());

            result = string.Compare(option, "/help", StringComparison.OrdinalIgnoreCase);
            _testOutputHelper.WriteLine(result.ToString());

            bool isPhd = lastName.EndsWith("Ph.D.");
            bool isDr = lastName.StartsWith("Dr.");
            _testOutputHelper.WriteLine(isPhd.ToString());
            _testOutputHelper.WriteLine(isDr.ToString());

            string severity = "warning";
            _testOutputHelper.WriteLine(severity.ToUpper());
            _testOutputHelper.WriteLine(severity.ToLower());

            string username = "";
            _testOutputHelper.WriteLine(username.Trim());
            _testOutputHelper.WriteLine(username.TrimStart());
            _testOutputHelper.WriteLine(username.TrimEnd());

            string filename = "";
            _testOutputHelper.WriteLine(filename.Replace("?", ""));

            _testOutputHelper.WriteLine(Environment.NewLine);

            string palindrome;
            _testOutputHelper.WriteLine("Enter a palindrome");
            palindrome = Console.ReadLine();
            Debug.Assert(palindrome != null, nameof(palindrome) + " != null");
            _testOutputHelper.WriteLine($"The palindrome, \"{palindrome}\" is {palindrome.Length} characters.");

            StringBuilder sb = new StringBuilder(10);
            sb.Append("");
            sb.Append("");
            _testOutputHelper.WriteLine(sb.ToString());

        }

        [Fact]
        public void VarDemo()
        {
            var text = Console.ReadLine();
            var uppercase = text?.ToUpper();
            _testOutputHelper.WriteLine(uppercase);

            var patent1 = new {Title = "Bifocals", YearOfPublication = "1784"};
            var patent2 = new {Title = "Phonograph", YearOfPublication = "1877"};
            _testOutputHelper.WriteLine($"{patent1.Title} ({patent1.YearOfPublication})");
            _testOutputHelper.WriteLine($"{patent2.Title} ({patent2.YearOfPublication})");
        }

        [Fact]
        public void CheckedDemo()
        {
            checked
            {
                // int.MaxValue equals 2147483647
                int n = int.MaxValue;
                n = n + 1;
                _testOutputHelper.WriteLine(n.ToString());
            }
        }

        [Fact]
        public void UnCheckedDemo()
        {
            unchecked
            {
                // int.MaxValue equals 2147483647
                int n = int.MaxValue;
                n = n + 1;
                _testOutputHelper.WriteLine(n.ToString());
            }
        }

        [Fact]
        public void ConvertDemo()
        {
            int intNumber = 31416;
            long longNumber = intNumber;
            _testOutputHelper.WriteLine(longNumber.ToString());
            
            longNumber = (long) intNumber;
            _testOutputHelper.WriteLine(longNumber.ToString());

            string text = "9.11E-31";
            float kgElectronMass = float.Parse(text);
            _testOutputHelper.WriteLine(longNumber.ToString());

            string middleCText = "261.626";
            double middleC = Convert.ToDouble(middleCText);
            _testOutputHelper.WriteLine(middleC.ToString());
            bool boolean = Convert.ToBoolean(middleC);
            _testOutputHelper.WriteLine(boolean.ToString());

            text = boolean.ToString();
            _testOutputHelper.WriteLine(text);

            double number;
            string input;
            input = Console.ReadLine();
            if (double.TryParse(input, out number))
            {
                // 转换成功
            }
            else
            {
                // 转换失败
            }
        }

        [Fact]
        public void ArrayDemo()
        {
            string[] languages =
            {
                "C#", "COBOL", "Java", "C++", "Visual Basic", "Pascal",
                "Fortran", "Lisp", "J#"
            };
            languages = new string[9];
            languages = new[]
            {
                "C#", "COBOL", "Java", "C++", "Visual Basic", "Pascal",
                "Fortran", "Lisp", "J#"
            };
            
            int[,] cells = new int[3, 3];
            cells = new int[,]
            {
                {1, 0, 2},
                {1, 2, 0},
                {1, 2, 1}
            };

            int count = default(int);

            string language = languages[3];
            languages[3] = languages[2];
            languages[2] = language;
            
            _testOutputHelper.WriteLine($"There are {languages.Length} " +
                                        $"languages in the array.");
            
            Array.Sort(languages);

            string searchString = "COBOL";
            int index = Array.BinarySearch(languages, searchString);
            _testOutputHelper.WriteLine($"The wave of the future, {searchString}, " +
                                        $"is at index {index}.");
            _testOutputHelper.WriteLine("");
            _testOutputHelper.WriteLine("{0,-20}\t{1,-20}", "First Element", "Last Element");
            _testOutputHelper.WriteLine("{0,-20}\t{1,-20}", "-------------", "------------");
            _testOutputHelper.WriteLine("{0,-20}\t{1,-20}", languages[0], languages[^1]);
            Array.Reverse(languages);
            _testOutputHelper.WriteLine("{0,-20}\t{1,-20}", languages[0], languages[^1]);
            Array.Clear(languages, 0, languages.Length);
            _testOutputHelper.WriteLine("{0,-20}\t{1,-20}", languages[0], languages[^1]);
            _testOutputHelper.WriteLine("After clearing, the array size is: {0}", languages.Length);

            bool[,,] newCells = new bool[2, 3, 3];
            _testOutputHelper.WriteLine(newCells.GetLength(0).ToString());
            _testOutputHelper.WriteLine(newCells.Rank.ToString());
            _testOutputHelper.WriteLine(newCells.Clone().ToString());
        }

        [Fact]
        public void StringReverseDemo()
        {
            string reverse, palindrome;
            char[] temp;
            
            _testOutputHelper.WriteLine("Enter a palindrome: ");
            palindrome = Console.ReadLine();

            reverse = palindrome?.Replace(" ", "");
            reverse = reverse?.ToLower();

            temp = reverse?.ToCharArray();
            Array.Reverse(temp ?? Array.Empty<char>());

            if (reverse == new string(temp))
            {
                _testOutputHelper.WriteLine("\"{0}\" is a palindrome.", palindrome);
            }
            else
            {
                _testOutputHelper.WriteLine("\"{0}\" is NOT a palindrome.", palindrome);
            }
        }
    }
}