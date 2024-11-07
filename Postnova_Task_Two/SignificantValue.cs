using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postnova_Task_Two
{
    public class SignificantValue
    {
        public double CalculateSignificantFigures(double y,int p)
        {
            if (y == 0) return 0;
            
            int n = (int)Math.Floor(Math.Log10(Math.Abs(y))) - 1 - p;
            double significantFigureValue = Math.Pow(10, n) * Math.Round(y / Math.Pow(10, n));

            return significantFigureValue;
        }
    }
}
