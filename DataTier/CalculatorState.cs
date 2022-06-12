using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator.DataTier
{
    enum Base
    {
        Two = 2,      //binary
        Eight = 8,    // octal
        Sixteen = 16, //hexadecimal
        Ten = 10      // decimal
    }

     public class CalculatorState
    {
        //To Take the status of current command and current button index
        public static  string currentCommand = "";
        public static string currentBtnIndex = "";

        //Status of which page is currently on Display
        public static bool isSecondPage = false;

        public static int countOfBracket = 0;

        //status of calculator mode i.e Radian or Degree
        public static bool isDegree = false;

        //Evluation string which is used in the evluation
        public static string evaluationString = "";

        public static string result = "";


    }
}
