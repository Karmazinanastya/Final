using Microsoft.VisualStudio.TestTools.UnitTesting;
using Final;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_2and3_5returned()
        {
            double result = Final.Calculator.EvaluateExpression("2+3");
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Subtract_5and2_3returned()
        {
            double result = Final.Calculator.EvaluateExpression("5-2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Multiplicat_2and3_6returned()
        {
            double result = Final.Calculator.EvaluateExpression("2*3");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Division_6and2_3returned()
        {
            double result = Final.Calculator.EvaluateExpression("6/2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Exponentiat_2and3_8returned()
        {
            double result = Final.Calculator.EvaluateExpression("2^3");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void sin_0_0returned()
        {
            double result = Final.Calculator.EvaluateExpression("sin(0)");
            Assert.AreEqual(Math.Sin(0), result, 0);
        }

        [TestMethod]
        public void Log_10_returned()
        {
            double result = Final.Calculator.EvaluateExpression("log(10)");
            Assert.AreEqual(Math.Log(10), result, 0.001);
        }

        [TestMethod]
        public void ComplexExpression()
        {
            double result = Final.Calculator.EvaluateExpression("2+3*4/(1+1)^2");
            Assert.AreEqual(5, result);
        }
    }
}