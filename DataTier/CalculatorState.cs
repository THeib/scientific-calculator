﻿
using Scientific_Calculator.interfaces;

namespace Scientific_Calculator.DataTier
{

     public class CalculatorState : ICalculatorState
    {



        public string CurrentCommand
        {
            get;
            set;
        }



        public string CurrentBtnIndex
        {
            get;
            set;
        }



      public  bool IsDegree
        {
            get;
            set;
        }

       public  string Expression
        {
            get;
            set;
        }
       public string Result
        {
            get;
            set;
        }

        public string EvaluationString
        {
            get;
            set;
        }

        public int CountOfBracket
        {
            get;
            set;
        }



    }
}
