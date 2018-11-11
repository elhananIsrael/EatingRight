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
using BE.function;
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
            MySearchFood = new ObservableCollection<FoodItem>();
            MyFoodToday = new ObservableCollection<FoodItem>();
            SelectedSearchFood =new FoodItem();




        PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }


        public BL.API.getFoodDetailsBL myAPI;
        private IEventAggregator _eventAggregator;
        private FoodItem selectedSearchFood;
        public ObservableCollection<FoodItem> mySearchFood;
        public ObservableCollection<FoodItem> myFoodToday;

        
        public Func<ChartPoint, string> PointLabel { get; set; }
        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();

            }
        }

        private bool CanAdd(Type arg)
        {
          //  if(SelectedSearchFood!=null && SelectedSearchFood.Name.Length>0)
            return true;
          //  return false;
        }

        private void RunAddSelectedFood(Type obj)
        {
            throw new NotImplementedException();
        }

        private bool CanSearch(Type arg)
        {
            //  return !string.IsNullOrEmpty(Search);
            return true;
         //לבדוק אם רשימה לא ריקה;
            
        }

        private void RunSearchFood(Type obj)
        {
            GetFoodFromApiToOurList();
        }



       
        public ObservableCollection<FoodItem>MySearchFood
        {
            get { 
                return mySearchFood;
            }
            set
            {
                mySearchFood =value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<FoodItem> MyFoodToday
        {
            get
            {
                return myFoodToday;
            }
            set
            {
                myFoodToday = value;
                OnPropertyChanged();

            }
        }

        public FoodItem SelectedSearchFood
        {
            get
            {
                return selectedSearchFood;
            }
            set
            {
               var temp = value;
                double? num;
                num = temp.Nutritions.Calories;
                temp.Nutritions.Calories = MyFunction.DoubleNumberNotNull(num);

                num = temp.Nutritions.Carbohydrate;
                temp.Nutritions.Carbohydrate = MyFunction.DoubleNumberNotNull(num);

                num = temp.Nutritions.Fat;
                temp.Nutritions.Fat = MyFunction.DoubleNumberNotNull(num);

                num = temp.Nutritions.Protein;
                temp.Nutritions.Protein = MyFunction.DoubleNumberNotNull(num);

                num = temp.Nutritions.Sodium;
                temp.Nutritions.Sodium = MyFunction.DoubleNumberNotNull(num);

                num = temp.Nutritions.Sugar;
                temp.Nutritions.Sugar = MyFunction.DoubleNumberNotNull(num);
                selectedSearchFood= temp;
                OnPropertyChanged();

            }
        }


        public async void  GetFoodFromApiToOurList()
        {
            mySearchFood.Clear();
            var answer = await myAPI.SearchFoodByName(Search);
            foreach (var food in answer)
            {
                food.Nutritions=await myAPI.GetNutritionByName(food.Name);
                mySearchFood.Add(food);
            }

        }

     
         







        

           


        //////////////////////////////////////////// Commands:
        public ICommand SearchFoodCommand { get; }
        public ICommand AddSelectedFoodCommand { get; }
       

    }
}

