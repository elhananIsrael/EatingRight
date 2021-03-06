﻿using System;
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
using PL.Tools;



namespace PL.ViewModel
{
    class AddFoodVM:BaseVM
    {
        public AddFoodVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;

            myBl = new Bl();

            SearchFoodCommand = new DelegateCommand<Type>(RunSearchFood, CanSearch);
            AddSelectedFoodCommand = new DelegateCommand<Type>(RunAddSelectedFood, CanAdd);
            MySearchFood = new ObservableCollection<FoodItem>();
            MyFoodToday = new ObservableCollection<FoodItem>();
            SelectedSearchFood =new FoodItem();
            selectedMyFood = new FoodItem();
            showSelectedFood = new FoodItem();

        PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        public Bl myBl;
        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;
        private FoodItem selectedSearchFood;
        private FoodItem showSelectedFood;
        private FoodItem selectedMyFood;
        private Meal myMeal;
        public string SearchFoodRightNow { get; set; }
        public ObservableCollection<FoodItem> mySearchFood;
        public ObservableCollection<FoodItem> myFoodToday;

        
        public Func<ChartPoint, string> PointLabel { get; set; }
        private string search;

        public DateTime SelectedDate
        {
            get {
                if (myMeal == null)
                {
                    updateMyMeal();
                }                
                return myMeal.Date;
            }
            set {
                if (value != null && value != myMeal.Date)
                {
                    myMeal = new Meal();
                    myMeal.Date = value;
                    updateMyMeal();
                    OnPropertyChanged();
                }
            }
        }

        public Meal MyMeal {
            get {
                if (myMeal == null)
                {
                    updateMyMeal();
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
            Meal tempMeal = new Meal();
            FoodItem temp = new FoodItem();
            temp.Id= Guid.NewGuid();
            temp.ImageUrl = selectedSearchFood.ImageUrl;
            temp.Name = selectedSearchFood.Name;
            temp.Nutritions = selectedSearchFood.Nutritions;
            temp.TagId = selectedSearchFood.TagId;
            
            myMeal.FoodItems.Add(temp);
            tempMeal.Date = myMeal.Date;
            tempMeal.FoodItems.Add(temp);

            updateMyFoodToday();
            await myBl.AddMeal(tempMeal);
            await _myMessageDialog.ShowInfoDialogAsync("Food Added!");
            OnPropertyChanged();

        }


        private bool CanSearch(Type arg)
        {
            return !string.IsNullOrEmpty(Search);
        }

        private async  void RunSearchFood(Type obj)
        {
            SearchFoodRightNow = Search;
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
                if (value != null)
                {
                    var temp = value;
                    double? num;
                    num = temp.Nutritions.Calories;
                    temp.Nutritions.Calories = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Carbohydrate;
                    temp.Nutritions.Carbohydrate = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Fat;
                    temp.Nutritions.Fat = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Protein;
                    temp.Nutritions.Protein = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Sodium;
                    temp.Nutritions.Sodium = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Sugar;
                    temp.Nutritions.Sugar = MyFunctions.DoubleNumberNotNull(num);
                    selectedSearchFood = temp;
                    ShowSelectedFood = temp;
                    OnPropertyChanged();
                    ((DelegateCommand<Type>)AddSelectedFoodCommand).RaiseCanExecuteChanged();

                }

            }
        }
        public FoodItem SelectedMyFood
        {
            get
            {
                return selectedMyFood;
            }
            set
            {
                if (value != null)
                {
                    var temp = value;
                    double? num;
                    num = temp.Nutritions.Calories;
                    temp.Nutritions.Calories = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Carbohydrate;
                    temp.Nutritions.Carbohydrate = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Fat;
                    temp.Nutritions.Fat = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Protein;
                    temp.Nutritions.Protein = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Sodium;
                    temp.Nutritions.Sodium = MyFunctions.DoubleNumberNotNull(num);

                    num = temp.Nutritions.Sugar;
                    temp.Nutritions.Sugar = MyFunctions.DoubleNumberNotNull(num);
                    selectedMyFood = temp;
                    ShowSelectedFood = temp;
                    OnPropertyChanged();
                }
            }
        }


        public FoodItem ShowSelectedFood
        {
            get
            { return showSelectedFood; }
            set
            {
                showSelectedFood = value;
                OnPropertyChanged();
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
            try
            {
                var curSearch = SearchFoodRightNow;
                ObservableCollection<FoodItem> tempSearch = new ObservableCollection<FoodItem>();
                string mySearch = search;
                var answer = await myBl.GetFoodItems(Search);
                if (answer.Count == 0)
                {
                    MySearchFood = new ObservableCollection<FoodItem>();
                    await _myMessageDialog.ShowInfoDialogAsync("No results found for " + "\"" + Search + "\"");
                }
                else
                {
                    MySearchFood = new ObservableCollection<FoodItem>();

                    foreach (var food in answer)
                    {
                        if (curSearch == SearchFoodRightNow)
                        {
                            food.Nutritions = await myBl.GetNutritions(food.Name);
                            mySearchFood.Add(food);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                await _myMessageDialog.ShowInfoDialogAsync(ex.Message);
            }

        }       


        //////////////////////////////////////////// Commands:
        public ICommand SearchFoodCommand { get; }
        public ICommand AddSelectedFoodCommand { get; }
       

    }
}

