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
using BL;



namespace PL.ViewModel
{
    class AddFoodVM:BaseVM
    {
        public AddFoodVM()
        {
            _eventAggregator = new Prism.Events.EventAggregator();

            myBl = new Bl();

            SearchFoodCommand = new DelegateCommand<Type>(RunSearchFood, CanSearch);
            AddSelectedFoodCommand = new DelegateCommand<Type>(RunAddSelectedFood, CanAdd);
            MySearchFood = new ObservableCollection<FoodItem>();
            MyFoodToday = new ObservableCollection<FoodItem>();
            SelectedSearchFood =new FoodItem();
            
           // myDate = DateTime.Now;


        PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        public Bl myBl;
      //  public BL.API.getFoodDetailsBL myAPI;
        private IEventAggregator _eventAggregator;
        private FoodItem selectedSearchFood;
        private Meal myMeal;
       // private DateTime myDate;

        public ObservableCollection<FoodItem> mySearchFood;
        public ObservableCollection<FoodItem> myFoodToday;

        
        public Func<ChartPoint, string> PointLabel { get; set; }
        private string search;

        /*   public DateTime MyDate {
               get { return myDate; }
               set { myDate = value;
                   my

               } }*/

        public DateTime SelectedDate
        {
            get {
                if (myMeal == null)
                {
                    updateMyMeal();
                   // updateMyFoodToday();
                }                
                return myMeal.Date;
            }
            set {
                if (value != null)
                {
                    myMeal = new Meal();
                    myMeal.Date = value;
                    updateMyMeal();
                    //updateMyFoodToday();
                    OnPropertyChanged();
                }
            }
        }

        public Meal MyMeal {
            get {
                if (myMeal == null)
                {
                    updateMyMeal();
                   // updateMyFoodToday();
                }
                    return myMeal;
            }
            set {
                if(value !=null)
                myMeal =value;
                updateMyFoodToday();

                OnPropertyChanged();
            } }

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SearchFoodCommand).RaiseCanExecuteChanged();
            }
        }

        private bool CanAdd(Type arg)
        {
            if(SelectedSearchFood!=null && SelectedSearchFood.Name.Length>0)
            return true;
            return false;
        }

        private async void RunAddSelectedFood(Type obj)
        {
           await AddSelectedFood();
        }

        private async Task AddSelectedFood()
        {
            FoodItem temp = new FoodItem();
            temp.Id= Guid.NewGuid();
            temp.ImageUrl = selectedSearchFood.ImageUrl;
            temp.Name = selectedSearchFood.Name;
            temp.Nutritions = selectedSearchFood.Nutritions;
            temp.TagId = selectedSearchFood.TagId;
            
            //selectedSearchFood.Id= Guid.NewGuid();
            MyMeal.FoodItems.Add(temp);
            updateMyFoodToday();
            await myBl.AddMeal(myMeal);
            OnPropertyChanged();
        }


        private bool CanSearch(Type arg)
        {
              return !string.IsNullOrEmpty(Search);
           // return true;
         //לבדוק אם רשימה לא ריקה;
            
        }

        private async  void RunSearchFood(Type obj)
        {
          await GetFoodFromApiToOurList();
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
                ((DelegateCommand<Type>)AddSelectedFoodCommand).RaiseCanExecuteChanged(); 


            }
        }

        public async  Task updateMyMeal()
        {
            if (myMeal==null)
            myMeal = new Meal();
            var tempMeal = await myBl.GetMeal(myMeal.Date);
            if (tempMeal != null)
                myMeal = tempMeal;
            updateMyFoodToday();
        }


       public void updateMyFoodToday()
        {
            myFoodToday.Clear();
            foreach (var food in myMeal.FoodItems)
            {
                MyFoodToday.Add(food);
            }
           
        }
       

        public async Task  GetFoodFromApiToOurList()
        {
            mySearchFood.Clear();
            var answer = await myBl.GetFoodItems(Search);
            foreach (var food in answer)
            {
                food.Nutritions=await myBl.GetNutritions(food.Name);
                mySearchFood.Add(food);
            }

        }

     
         







        

           


        //////////////////////////////////////////// Commands:
        public ICommand SearchFoodCommand { get; }
        public ICommand AddSelectedFoodCommand { get; }
       

    }
}

