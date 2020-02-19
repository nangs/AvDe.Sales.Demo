using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AvDe.Demo.Tests.Services.XUnitUtilities
{
    // inspired by: 
    // http://www.tomdupont.net/2016/04/how-to-order-xunit-tests-and-collections.html
    // https://github.com/contentful/Contentful.Net.Integration/blob/master/Contentful.Net.CMA/XunitUtilities.cs
    public class CustomTestCaseOrderer : ITestCaseOrderer
    {
        //public const string TYPE_NAME = "AvDe.Demo.Tests.XUnitUtilities.CustomTestCaseOrderer";
        //public const string ASSEMBY_NAME = "AvDe.Demo.Tests";
        private static readonly ConcurrentDictionary<string, ConcurrentQueue<string>> _queuedTests = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            return testCases.OrderBy(GetOrder);
        }

        private static int GetOrder<TTestCase>(
            TTestCase testCase)
            where TTestCase : ITestCase
        {
            // Enqueue the test name.
            _queuedTests
                .GetOrAdd(
                    testCase.TestMethod.TestClass.Class.Name,
                    key => new ConcurrentQueue<string>())
                .Enqueue(testCase.TestMethod.Method.Name);

            // Order the test based on the attribute.
            var attr = testCase.TestMethod.Method
                .ToRuntimeMethod()
                .GetCustomAttribute<OrderAttribute>();
            return attr?.I ?? 0;
        }
    }
}