using System;
using Xunit;
using Xunit.Abstractions;

namespace CSharpStudy
{
    public class Program
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Program(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void HelloWorld()
        {
            _testOutputHelper.WriteLine("Hello. My name is Inigo Montoya.");
        }

        [Fact]
        public void ReadLine()
        {
            string firstName;
            string lastName;
            _testOutputHelper.WriteLine("hey you!");
            
            _testOutputHelper.WriteLine("Enter your first name: ");
            firstName = Console.ReadLine();
            
            _testOutputHelper.WriteLine("Enter your last name: ");
            lastName = Console.ReadLine();
            
            _testOutputHelper.WriteLine("Your full name {1}, {0}", firstName, lastName);
            // ...
        }
    }
}