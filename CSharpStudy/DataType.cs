using System;
using System.Diagnostics;
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
        public void Basic()
        {
            
        }

        [Fact]
        public void RoundTrip()
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
        public void String()
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
        }
    }
}