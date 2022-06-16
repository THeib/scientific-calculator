using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator.interfaces
{
    internal interface ICalculatorWithHistory
    {


        void UpdateExpression(string ex);

        void ResetResult();


        double Evaluate(string expression);

        void clearHistory(string newValiue);

        void changeSign(string expression);

        void bracketsClick(string expression);
        string BackSpace(string expression);
        void updateExpression(string value);
        void updateResult(string newResult);
        void forwCountOfBracket();

        void updateCountOfBracket(int number);



    }
}
