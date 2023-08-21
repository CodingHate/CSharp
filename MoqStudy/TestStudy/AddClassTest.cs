using Moq;
using MoqStudy;

namespace TestStudy
{
    [TestClass]
    public class AddClassTest
    {
        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var addMock = new Mock<ICalculator>();
            addMock.Setup(calculator => calculator.Add(2, 3)).Returns(5);

            // 모의 객체 사용
            ICalculator calculator = addMock.Object;

            int result = calculator.Add(2, 3);
            Assert.AreEqual(5, result);
            //// Act
            //Calculator calculator1 = new Calculator();
            //int result = calculator1.Test(calculator, 3, 5);

            //// Assert
            //Assert.AreEqual(8, result);
        }
    }
}
