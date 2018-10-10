using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Converters
{
    class FieldsToStudentConverter_EXAMPLE
    {
    }
}


/*
 
    using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestMVVM.Converters
{
    public class FieldsToStudentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new Student();
            result.Name = values[0].ToString();
           // result.Average = System.Convert.ToSingle(values[1]);
            result.Topic = values[2].ToString();
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


  */
