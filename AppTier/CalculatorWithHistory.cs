
using System;
using System.Data;
using System.Text.RegularExpressions;
using Scientific_Calculator.interfaces;
namespace Scientific_Calculator.AppTier
{
    internal class CalculatorWithHistory:BaseCalculator, ICalculatorWithHistory
    {

        private ICalculatorState calcHistory;
        internal string expression;

        public CalculatorWithHistory(ICalculatorState calcHistory)
        {  
            this.calcHistory = calcHistory;
        }


        public void clearHistory(string newValiue)
        {
            calcHistory.Result = "";
            calcHistory.CountOfBracket = 0;
            calcHistory.EvaluationString = "";
            calcHistory.Expression = newValiue;


        }

        public ICalculatorState getHistort()
        {
            return calcHistory;

        }

        public void changeSign(string expression) {

            Regex regex = new Regex(@"[+\-%*/(]$");
            if (expression.Length == 0)
            {
                UpdateExpression("(-");
                calcHistory.CountOfBracket++;
            }
            else if (regex.IsMatch(expression))

            {
                UpdateExpression("(-");
                calcHistory.CountOfBracket++;


            }
            else if (expression.EndsWith(")"))
            {
                UpdateExpression("*(-");
                calcHistory.CountOfBracket++;
            }

        }

        public void bracketsClick(string expression)
        {
            //Regular Expression to check if string ends with any algebric opertaor
            Regex regex = new Regex(@"[+\-% */]$");
            //Regular expression to check if strikng ends with numeric digit
            Regex numericEnd = new Regex(@"\d$");

            //if Expression is empty
            if (calcHistory.CountOfBracket == 0 && expression.Length == 0)
            {
                UpdateExpression("(");
                forwCountOfBracket();
            }

            //If opening bracket is on last index
            else if (expression.LastIndexOf('(') == expression.Length - 1)
            {
                UpdateExpression("(");
               forwCountOfBracket();
            }
            //If string ends with algebric operator
            else if (regex.IsMatch(expression))
            {
                UpdateExpression("(");
                forwCountOfBracket();
            }
            //
            else if (calcHistory.CountOfBracket == 0 && numericEnd.IsMatch(expression))
            {
                UpdateExpression("*(");
                forwCountOfBracket();

            }
            //Otherwise push closing bracket
            else
            {
                UpdateExpression(")");
                DecCountOfBracket();
            }


        }

        public string BackSpace(string expression)
        {

                string tempExpression = expression;

                if (tempExpression[tempExpression.Length - 1] == '(')
                {
                    calcHistory.CountOfBracket = 0;
                }

                if (tempExpression[tempExpression.Length - 1] == ')')
                {
                    calcHistory.CountOfBracket++;
                }


                string result= tempExpression.Remove(tempExpression.Length - 1);

                calcHistory.EvaluationString = result;
                calcHistory.Result = "";
                return result;

        }



        public void updateExpression(string value)
        {
            calcHistory.Expression = value;


        }

        public void updateResult(string newResult)
        {
            calcHistory.Result = newResult;


        }


        public void forwCountOfBracket()
        {

            calcHistory.CountOfBracket++;
        }
        public void DecCountOfBracket()
        {

            calcHistory.CountOfBracket--;
        }

        public void updateCountOfBracket(int number)
        {
            calcHistory.CountOfBracket = number;


        }

        public void UpdateExpression(string ex)
        {
  
            calcHistory.Expression += ex;
   
        }

        public void ResetResult()
        {
            calcHistory.Result = "";

        }

        public double Evaluate(string expression)
        {
            if (calcHistory.CurrentCommand != "")
            {
                Parse(expression);

            }

            DataTable dt = new DataTable();
            double  result  = Convert.ToDouble(dt.Compute(calcHistory.EvaluationString, String.Empty)); ;
            calcHistory.Result = Math.Round(result, 15).ToString();
            return result;

        }

        private void Parse(string expression)
        {

            int status = int.Parse(string.Concat(1, calcHistory.CurrentBtnIndex));


            switch (status)
            {
                case 102:
                    ParseSquareRoot();
                    break;
                case 110:
                    ParseSin();
                    break;
                case 111:
                    ParseCose();
                    break;

                case 112:
                    ParseTan();
                    break;
                case 120:
                    ParseLan();
                    break;
                case 121:
                    ParseLog();
                    break;
                case 130:
                    parseE();
                    break;
                case 140:
                    parseAbsoute();
                    break;
                case 131:
                case 132:
                case 241:
                    parseOther( expression);
                    break;
                default: { break; }
            }
        }

        private void ParseSquareRoot()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("sqrt(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            //Where sqrt( length is 5 so
            for (index = (startPosition + 5); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            result = Math.Round(SquareRoot(number), 2);
            calcHistory.EvaluationString= calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
        }


        private void ParseSin()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("sin(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            for (index = (startPosition + 4); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);

            if (calcHistory.IsDegree) { number = DegreeToRadian(number); }
            result = Math.Round(Sin(number), 2);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());

        }

        private void ParseCose()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("cos(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            for (index = (startPosition + 4); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            if (calcHistory.IsDegree) { number = DegreeToRadian(number); }
            result = Math.Round(Cos(number), 2);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());

        }
        private void ParseTan()
        {

            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("tan(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);

            for (index = (startPosition + 4); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }

            number = double.Parse(parsedNumber);
      
            if (calcHistory.IsDegree) { number = DegreeToRadian(number); }
            result = Math.Round(Tan(number), 2);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());



        }
        private void ParseLan()
        {

            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;

            startPosition = calcHistory.EvaluationString.IndexOf("ln(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            for (index = (startPosition + 3); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            result = Math.Round(Ln(number), 4);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());

        }

        private void ParseLog()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;

            startPosition = calcHistory.EvaluationString.IndexOf("log(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);

            for (index = (startPosition + 4); index < endPosition; index++)
            {

                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            result = Math.Round(Log(number), 4);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
    

        }
        private void parseE()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("e^(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            for (index = (startPosition + 3); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            result = Math.Round(Exponential(number), 5);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
        

        }
        private void parseAbsoute()
        {
            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            startPosition = calcHistory.EvaluationString.IndexOf("abs(");
            endPosition = calcHistory.EvaluationString.IndexOf(')', startPosition);
            for (index = (startPosition + 4); index < endPosition; index++)
            {
                parsedNumber += calcHistory.EvaluationString[index];
            }
            number = double.Parse(parsedNumber);
            result = Absolute(number);
            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(startPosition, (endPosition - startPosition + 1)).Insert(startPosition, result.ToString());
   
        }


        private void parseOther(string expression)
        {

            int startPosition, endPosition, index;
            string parsedNumber = "";
            double number, result;
            string givenNumber = "";
            startPosition = calcHistory.EvaluationString.IndexOf("^(");
            endPosition = calcHistory.EvaluationString.IndexOf(")", startPosition);
            for (index = (startPosition + 2); index < endPosition; index++)
            {
   
                parsedNumber += calcHistory.EvaluationString[index];
            }
            double power = double.Parse(parsedNumber);

            index = startPosition - 1;

            while (index >= 0 && (calcHistory.EvaluationString[index] != '+' &&  expression[index] != '-' && expression[index] != '*' && expression[index] != '/' && expression[index] != '%' && expression[index] != '(' && expression[index] != ')'))
            {
                givenNumber += calcHistory.EvaluationString[index];
                index--;
            }

            result = Math.Round(Power(double.Parse(givenNumber), power), 2);

            calcHistory.EvaluationString = calcHistory.EvaluationString.Remove(index + 1, endPosition - index).Insert(index + 1, result.ToString());




        }

    }
}
