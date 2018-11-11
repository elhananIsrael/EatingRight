using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.function
{
    public class MyFunction
    {

        public static double DoubleNumberNotNull(double? num)
        {
            double? number = 0;
            if (num == null)
                return number.Value;
            else return num.Value;
        }
    }
}
