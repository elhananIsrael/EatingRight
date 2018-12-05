using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using System.Globalization;
using System.Windows.Data;
using BE.Entitys;
using PL.ViewModel;
using LiveCharts.Wpf;
using System.Windows.Media;
using BE.function;

namespace PL.Converters
{
    public class Object2ChartValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object res = null;

            if (value is FoodItem SelectedSearchFood)
            {
                PieChart pc = new PieChart();
                pc.Series = new SeriesCollection{ new PieSeries  { Title = "Calories", Fill = Brushes.Red, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Calories.Value) } },
                new PieSeries { Title = "Carbohydrate", Fill = Brushes.Green, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Carbohydrate.Value) } },
                new PieSeries { Title = "Fat", Fill = Brushes.Purple, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Fat.Value) } },
                new PieSeries { Title = "Protein", Fill = Brushes.Maroon, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Protein.Value) } },
                new PieSeries { Title = "Sodium", Fill = Brushes.Yellow, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Sodium.Value) } },
                new PieSeries { Title = "Sugar", Fill = Brushes.DeepPink, StrokeThickness = 0, Values = new ChartValues<double> { MyFunctions.DoubleNumberNotNull(SelectedSearchFood.Nutritions.Sugar.Value) } } }    ;
                res = pc.Series;
            }

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

       

    }



}