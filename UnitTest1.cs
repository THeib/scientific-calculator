using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Scientific_Calculator.DataTier;
using Scientific_Calculator.AppTier;
using Scientific_Calculator.PreTier;
using Scientific_Calculator;


namespace TestCalculate
{
    [TestClass]
    public class UnitTest1
    {
         

        [TestMethod]
        public void Test_Abs()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = calc.Absolute(-5);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_log()
        {
            var calc = new Calculator();
            double expectedValue = 2;
            double actualValue = calc.Log(4);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_ln()
        {
            var calc = new Calculator();
            double expectedValue = 0;
            double actualValue = calc.Ln(1);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_Power()
        {
            var calc = new Calculator();
            double expectedValue = 16;
            double actualValue = calc.Power(4, 2);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_Sqart()
        {
            var calc = new Calculator();
            double expectedValue = 4;
            double actualValue = calc.SquareRoot(16);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_Cos()
        {
            var calc = new Calculator();
            double expectedValue = 0.5;

            double actualValue = calc.Cos(calc.DegreeToRadian(60));
            Assert.AreEqual(expectedValue, calc.Round(actualValue, 5));

        }
        [TestMethod]
        public void Test_Sin()
        {
            var calc = new Calculator();
            double expectedValue = 0.5;
            double actualValue = calc.Sin(calc.DegreeToRadian(30));
            Assert.AreEqual(expectedValue, calc.Round(actualValue, 5));
        }
        [TestMethod]
        public void Test_Tan()
        {
            var calc = new Calculator();
            double expectedValue = 1;
            double actualValue = calc.Tan(calc.DegreeToRadian(45));
            Assert.AreEqual(expectedValue, calc.Round(actualValue, 5));
        }
        [TestMethod]
        public void Test_Sum()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = 3.3 + 1.7;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_Sub()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = 15 - 10;
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_multiply()
        {
            var calc = new Calculator();
            double expectedValue = 15;
            double actualValue = 5 * 3;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_Divide()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = 25 / 5;
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_Exponentiation()
        {
            var calc = new Calculator();
            double expectedValue = 2.7182818284590451;

            double actualValue = calc.E();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_PI()
        {
            var calc = new Calculator();
            double expectedValue = 3.141592653589793;
            double actualValue = calc.PI();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Test_RadToDegree()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = calc.Absolute(-5);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        public void Test_Percent()
        {
            var calc = new Calculator();
            double expectedValue = 5;
            double actualValue = calc.Absolute(-5);
            Assert.AreEqual(expectedValue, actualValue);

        }

        [TestMethod]
        public void TestHistoryLOGGER()
        {

            //Logger logger = new Logger();
            //logger.addNewEntry("5+5");
            CalculatorState.Expression = "5+5";
            string firstlLog = LoggerState.entities.ToArray()[0];
            Assert.AreEqual(CalculatorState.Expression,firstlLog);

        }

    

            /*
            [TestMethod]
            public void TestInvalidExpressions()
            {
                var calc = new MainForm();

                txtBoxExpression.Text = "oneOver(0)";
                Assert.ThrowsException<Calculator>(() => Scientific_Calculator.Form1());

                Calculator.SquareRoot(-10);
                Assert.ThrowsException<Calculator>(() => Calculator.SquareRoot(-10));
            }
            */





            /*

            [TestMethod]
            public void TestInvalidExpressions()
            {
                txtbox = "oneOver(0)";
                Assert.ThrowsException<MathException>(() => Calculator.Form1());

                txtBoxExpression = "sqrt(-10)";
                Assert.ThrowsException<MathException>(() => AppTier.MathExpression.Evaluate());
            }

            [TestMethod]
            public void TestHistoryLOGGER()
            {
                // Clear history
                CalculationHistory.Instance.History = new System.Collections.ObjectModel.ObservableCollection<CalculationResult>();

                CalculatorState.Instance.Expression = "4+3";
                ApplicationTier.MathExpression.Evaluate();

                CalculatorState.Instance.Expression = "8*3";
                ApplicationTier.MathExpression.Evaluate();

                CalculatorState.Instance.Expression = "10/2";
                ApplicationTier.MathExpression.Evaluate();

                CalculatorState.Instance.Expression = "9-12";
                ApplicationTier.MathExpression.Evaluate();

                Assert.AreEqual(CalculationHistory.Instance.History.Count, 4);

                Assert.AreEqual(CalculationHistory.Instance.History[0].Expression, "4+3");
                Assert.AreEqual(CalculationHistory.Instance.History[0].Result, "7");

                Assert.AreEqual(CalculationHistory.Instance.History[1].Expression, "8*3");
                Assert.AreEqual(CalculationHistory.Instance.History[1].Result, "24");

                Assert.AreEqual(CalculationHistory.Instance.History[2].Expression, "10/2");
                Assert.AreEqual(CalculationHistory.Instance.History[2].Result, "5");

                Assert.AreEqual(CalculationHistory.Instance.History[3].Expression, "9-12");
                Assert.AreEqual(CalculationHistory.Instance.History[3].Result, "-3");
            }

            [TestMethod]
            public void TestBasic()
            {
                CalculatorState.Instance.Radix = 10;
                CalculatorState.Instance.Expression = "4 + 3";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "7");

                CalculatorState.Instance.Expression = "7*4";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "28");

                CalculatorState.Instance.Expression = "6/3";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "2");

                CalculatorState.Instance.Expression = "(9-5) * 2";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "8");

                CalculatorState.Instance.Expression = "10%100";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "10");
            }

            [TestMethod]
            public void TestRadixOperations()
            {
                CalculatorState.Instance.Radix = 8;
                CalculatorState.Instance.Expression = "5+5";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "12");

                CalculatorState.Instance.Radix = 2;
                CalculatorState.Instance.Expression = "101+1";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "110");

                CalculatorState.Instance.Radix = 16;
                CalculatorState.Instance.Expression = "BE*95C";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "6F248");
            }

            [TestMethod]
            public void TestFunctions()
            {
                CalculatorState.Instance.Radix = 10;
                CalculatorState.Instance.Expression = "pow(4)+3";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "19");

                CalculatorState.Instance.Expression = "abs(-3)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "3");

                CalculatorState.Instance.Expression = "sqrt(16)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "4");

                CalculatorState.Instance.Expression = "powTo(3,5)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "243");

                CalculatorState.Instance.Expression = "tenTo(5)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "100000");

                CalculatorState.Instance.Expression = "log(10)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "2.302585092994046");

                CalculatorState.Instance.Expression = "ln(40)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "3.6888794541139363");

                CalculatorState.Instance.Expression = "oneOver(5)";
                Assert.AreEqual(ApplicationTier.MathExpression.Evaluate(), "0.2");
            }
             */


        }
}








