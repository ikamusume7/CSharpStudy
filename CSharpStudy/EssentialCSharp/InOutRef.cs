using System;
using Xunit;
using Xunit.Abstractions;

namespace EssentialCSharp
{
    public class InOutRef
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public InOutRef(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            string outString = "10";// out在函数外也可以赋值
            string refString = "1";// ref不初始化会报错
            Method(100, out outString, ref refString);
            _testOutputHelper.WriteLine(outString);
            _testOutputHelper.WriteLine(refString);
        }

        public void Method(in int intTest, out string outTest, ref string refTest)
        {
            _testOutputHelper.WriteLine(intTest.ToString());
            // _testOutputHelper.WriteLine(outTest); out关键字不能传递值
            _testOutputHelper.WriteLine(refTest);
            // intTest = 1; in关键字不能修改值
            outTest = "20";// out关键字不赋值会报错
            refTest = "2";// ref关键字不赋值不会报错
        }
    }
}