using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using Prism.Commands;
using PL.Events;
using PL.View;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using BE.Entitys;
using BL;
using System.Collections.ObjectModel;



namespace PL.ViewModel
{
    class AddFoodVM:BaseVM
    {
        public AddFoodVM()
        {
            _eventAggregator = new Prism.Events.EventAggregator();

            myAPI = new BL.API.getFoodDetailsBL();

            SearchFoodCommand = new DelegateCommand<Type>(RunSearchFood, CanSearch);
            AddSelectedFoodCommand = new DelegateCommand<Type>(RunAddSelectedFood, CanAdd);
            MySearchFoodList = new ObservableCollection<FoodItem>();




        PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }


        public BL.API.getFoodDetailsBL myAPI;
        private IEventAggregator _eventAggregator;
        public ObservableCollection<FoodItem> mySearchFoodList;
        public Func<ChartPoint, string> PointLabel { get; set; }
        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;

            }
        }

        private bool CanAdd(Type arg)
        {
            //לבדוק אם רשימה לא ריקה;
            return true;
        }

        private void RunAddSelectedFood(Type obj)
        {
            throw new NotImplementedException();
        }

        private bool CanSearch(Type arg)
        {
            //לבדוק אם רשימה לא ריקה;
            return true;
        }

        private void RunSearchFood(Type obj)
        {
            GetFoodFromApiToOurList();        }

        


        public ObservableCollection<FoodItem>MySearchFoodList
        {
            get { 
                return mySearchFoodList;
            }
    set
            {
                mySearchFoodList =value;
                OnPropertyChanged();

}
        }


public async void  GetFoodFromApiToOurList()
        {
            mySearchFoodList.Clear();
            var answer = await myAPI.SearchFoodByName(Search);
            foreach (var food in answer)
            {
                food.Nutritions=await myAPI.GetNutritionByName(food.Name);
                mySearchFoodList.Add(food);
            }

        }

     
         

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
    {
        var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

        //clear selected slice.
        foreach (PieSeries series in chart.Series)
            series.PushOut = 0;

        var selectedSeries = (PieSeries)chartpoint.SeriesView;
        selectedSeries.PushOut = 8;
        }







        

           


        //////////////////////////////////////////// Commands:
        public ICommand SearchFoodCommand { get; }
        public ICommand AddSelectedFoodCommand { get; }
       

    }
}

