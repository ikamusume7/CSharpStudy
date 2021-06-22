using System;
using System.Collections;
using Xunit;
using Xunit.Abstractions;

namespace EssentialCSharp
{
    public class ValueType
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ValueType(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            Angle angle = new Angle(25, 58, 23);
            object objectAngle = angle; // 装箱
            _testOutputHelper.WriteLine(((Angle)objectAngle).Degrees.ToString());
            
            // 拆箱
            ((Angle)objectAngle).MoveTo(26, 58, 23);
            
            // 装箱
            ((IAngle)angle).MoveTo(26, 58, 23);
            _testOutputHelper.WriteLine(", " + ((Angle) angle).Degrees);
            
            ((IAngle)objectAngle).MoveTo(26, 58, 23);
            _testOutputHelper.WriteLine(", " + ((Angle) objectAngle).Degrees);

        }
    }

    enum ConnectionState : short
    {
        Disconnected,
        Connecting = 10,
        Connected,
        Joined = Connected,
        DisConnecting
    }

    public class DisplayFibonacci
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public DisplayFibonacci(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Demo()
        {
            ArrayList list = new ArrayList();
            
            _testOutputHelper.WriteLine("Enter a number between 2 and 1000:");
            int totalCount = int.Parse(Console.ReadLine() ?? string.Empty);

            list.Add((double) 0);
            list.Add((double) 1);
            for (int count = 2; count < totalCount; count++)
            {
                list.Add((double) list[count - 1] + (double) list[count - 2]);
            }

            foreach (double count in list)
            {
                _testOutputHelper.WriteLine("{0}, ", count);
            }
        }
    }

    interface IAngle
    {
        void MoveTo(int degrees, int minutes, int seconds);
    }

    struct Angle : IAngle
    {
        private int _Degrees;
        private int _Minutes;
        private int _Seconds;

        public int Degrees => _Degrees;
        public int Minutes => _Minutes;
        public int Seconds => _Seconds;
        
        public Angle(int degrees, int minutes, int seconds)
        {
            _Degrees = degrees;
            _Minutes = minutes;
            _Seconds = seconds;
        }

        public Angle(int degrees, int minutes) : this(degrees, minutes, default(int))
        {
            
        }

        public Angle Move(int degrees, int minutes, int seconds)
        {
            return new Angle(Degrees + degrees, 
                Minutes + minutes, Seconds + seconds);
        }

        class Coordinate
        {
            private Angle _Longitude;
            private Angle _Latitude;
            
            public Angle Longitude
            {
                get => _Longitude;
                set => _Longitude = value;
            }

            public Angle Latitude
            {
                get => _Latitude;
                set => _Latitude = value;
            }
        }

        public void MoveTo(int degrees, int minutes, int seconds)
        {
            _Degrees = degrees;
            _Minutes = minutes;
            _Seconds = seconds;
        }
    }
}