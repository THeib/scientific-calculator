using System;

namespace Scientific_Calculator.interfaces
{
    internal interface IBaseCalculator
    {

        double SquareRoot(double Num);

        double Tan(double Num);

        double Cos(double Num);


        double Sin(double Num);

        double Ln(double Num);


        double Log(double Num);


        double Exponential(double Num);

        double Absolute(double Num);

        double PI();


        double E();

        double PowerOf2(double Num);


        double Power(double Num, Double power);

        double DegreeToRadian(double num);
        double RadianToDegree(double num);

        double Round(double num, int digit);


    }
}
