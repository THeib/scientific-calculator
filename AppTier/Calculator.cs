using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Calculator.AppTier
{
   public class Calculator
    {
      
        //Square Root
        public double SquareRoot(double Num)
        {
            return Math.Pow(Num,(double) 1 / 2);
            
        }
            
        //Tan
        public double Tan(double Num)
        {
            return Math.Tan(Num);
        }
        //Cos
        public double Cos(double Num)
        {
            return Math.Cos(Num);
        }
        //Sin
        public double Sin(double Num)
        {
            return Math.Sin(Num);
        }
        //Ln
        public double Ln(double Num)
        {
            return Math.Log(Num);
        }
        //Log
        public double Log(double Num)
        {
            return Math.Log(Num,2);
        }
        //Exponential
        public double Exponential(double Num)
        {
            return Math.Pow(Math.E,Num);
        }
        //Absolute
        public double Absolute(double Num)
        {
            return Math.Abs(Num);
        }
        //PI
        public double PI()
        {
            return Math.PI;
        }
        //E
        public double E()
        {
            return Math.E;
        }
        //PowerOf2
        public Double PowerOf2(double Num)
        {
            return Math.Pow(2, Num);
        }
        //Power
        public Double Power(double Num,Double power)
        {
            return Math.Pow(Num, power);
        }
        //Radian to Degree conversion
        public double DegreeToRadian(double num)
        {
            return num * (Math.PI /180);
        }
        //Radian to degree
        public double RadianToDegree(double num)
        {
            return num * (180 /Math.PI);

        }

        public double Round(double num , int digit)
        {
            return Math.Round(num , digit , MidpointRounding.AwayFromZero);
        }
       
      
    }


}
