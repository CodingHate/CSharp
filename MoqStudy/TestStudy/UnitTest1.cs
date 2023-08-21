using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqStudy;
using System.Text.RegularExpressions;

namespace TestStudy
{
    [TestClass]
    public class MoqTest
    {
        [TestMethod]
        public void Test01()
        {
            var mock = new Mock<IFoo>();
            IFoo foo = mock.Object;

            mock.Setup(foo => foo.DoSomething("ping")).Returns(true);

            // out arguments
            var outString = "ack";
            // TryParse will return true, and the out argument will return "ack", lazy evaluated
            // TryParse�� true�� ��ȯ�ϰ� out �μ��� "ack"�� ��ȯ�ϰ� ���� �򰡵˴ϴ�.
            mock.Setup(foo => foo.TryParse("ping", out outString)).Returns(true);


            // ref arguments
            var instance = new Bar();
            // Only matches if the ref argument to the invocation is the same instance
            // ȣ�⿡ ���� ref �μ��� ������ �ν��Ͻ��� ��쿡�� ��ġ�մϴ�.
            mock.Setup(foo => foo.Submit(ref instance)).Returns(true);
            //var valueInstance = mock.Object.Submit(ref instance);


            // access invocation arguments when returning a value
            // ���� ��ȯ�� �� ȣ�� �μ��� �׼���
            mock.Setup(x => x.DoSomethingStringy(It.IsAny<string>()))
                    .Returns((string s) => s.ToUpper());


            string result = foo.DoSomethingStringy("adsf");

            // Multiple parameters overloads available
            // ���� �Ű����� �����ε� ����

            // throwing when invoked with specific parameters
            // Ư�� �Ű������� ȣ��� �� �߻�
            mock.Setup(foo => foo.DoSomething("reset")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething("")).Throws(new ArgumentException("command"));


            // lazy evaluating return value
            // ���� �� ��ȯ ��
            var count = 1;
            mock.Setup(foo => foo.GetCount()).Returns(() => count);


            // async methods (see below for more about async):
            // �񵿱� �޼���(�񵿱⿡ ���� �ڼ��� ������ �Ʒ� ����):
            mock.Setup(foo => foo.DoSomethingAsync().Result).Returns(true);
        }

        [TestMethod]
        public void MatchingTest()
        {
            var mock = new Mock<IFoo>();

            //// any value
            //mock.Setup(foo => foo.DoSomething(It.IsAny<string>())).Returns(true);

            //// any value passed in a `ref` parameter (requires Moq 4.8 or later):
            //mock.Setup(foo => foo.Submit(ref It.Ref<Bar>.IsAny)).Returns(true);

            //// matching Func<int>, lazy evaluated
            //mock.Setup(foo => foo.Add(It.Is<int>(i => i % 2 == 0))).Returns(true);

            //// matching ranges
            //mock.Setup(foo => foo.Add(It.IsInRange<int>(0, 10, Range.Inclusive))).Returns(true);

            // matching regex
            mock.Setup(x => x.DoSomethingStringy(It.IsRegex("[a-d]+", RegexOptions.IgnoreCase))).Returns("foo");

            IFoo foo = mock.Object;

            var a = foo.DoSomethingStringy("eeeeee");

            Assert.Equals("foo", a);
        }

        [TestMethod]
        public void CallbackTest()
        {
            var mock = new Mock<IFoo>();
            var calls = 0;
            var callArgs = new List<string>();

            mock.Setup(foo => foo.DoSomething("ping"))
                .Callback(() => calls++)
                .Returns(true);

            // access invocation arguments
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback((string s) => callArgs.Add(s))
                .Returns(true);

            // alternate equivalent generic method syntax
            mock.Setup(foo => foo.DoSomething(It.IsAny<string>()))
                .Callback<string>(s => callArgs.Add(s))
                .Returns(true);

            // access arguments for methods with multiple parameters
            mock.Setup(foo => foo.DoSomething(It.IsAny<int>(), It.IsAny<string>()))
                .Callback<int, string>((i, s) => callArgs.Add(i.ToString()))
                .Returns(true)
                .Callback<int, string>((i, s) => callArgs.Add((i+100).ToString()));


            //// callbacks can be specified before and after invocation
            //mock.Setup(foo => foo.DoSomething("ping"))
            //    .Callback(() => Console.WriteLine("Before returns"))
            //    .Returns(true)
            //    .Callback(() => Console.WriteLine("After returns"));

            IFoo foo = mock.Object;
            var a = foo.DoSomething(1, "ping");
             
            Assert.IsTrue(a);
        }
    }
}