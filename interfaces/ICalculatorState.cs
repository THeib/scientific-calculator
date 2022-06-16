

namespace Scientific_Calculator.interfaces
{
    internal interface ICalculatorState
    {

        string CurrentCommand
        {
            get;
            set;
        }



        string CurrentBtnIndex
        {
            get;
            set;
        }



        bool IsDegree
        {
            get;
            set;
        }

        string Expression
        {
            get;
            set;
        }
        string Result
        {
            get;
            set;
        }

         string EvaluationString
        {
            get;
            set;
        }



         int CountOfBracket
        {
            get;
            set;
        }

    }
}
