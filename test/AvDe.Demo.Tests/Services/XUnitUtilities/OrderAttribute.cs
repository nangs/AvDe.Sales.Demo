using System;

namespace AvDe.Demo.Tests.Services.XUnitUtilities
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public int I { get; }

        public OrderAttribute(int i)
        {
            I = i;
        }
    }
}
