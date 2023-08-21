using Moq;
using MoqStudy;

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
    }
}