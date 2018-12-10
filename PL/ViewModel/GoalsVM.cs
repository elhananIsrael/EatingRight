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
using BL;
using BE.Entitys;
using PL.Tools;

namespace PL.ViewModel
{
    class GoalsVM:BaseVM
    {

        public GoalsVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
         

            myBl = new Bl();
            updateMyGoal();

            SaveGoalCommand = new DelegateCommand<Type>(RunSaveGoal, CanSaveGoal);           
        }

        public Bl myBl;
        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;

        private Goal myGoal;
        

        public DateTime SelectedDate
        {
            get
            {
                if (myGoal == null)
                {
                    updateMyGoal();
                }                
                return myGoal.Date;
            }
            set
            {
                if (value != null && value != myGoal.Date)
                {
                    myGoal = new Goal();
                    myGoal.Date = value;                   
                    updateMyGoal();
                    OnPropertyChanged("Calories");
                    OnPropertyChanged("Carbohydrate");
                    OnPropertyChanged("Fat");
                    OnPropertyChanged("Protein");
                    OnPropertyChanged("Sodium");
                    OnPropertyChanged("Sugar");
                    OnPropertyChanged();

                }
            }
        }

        

        public double? Calories
        {
            get
            {
                return myGoal.Calories;
            }
            set
            {
                if (value != null)
                    myGoal.Calories = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Carbohydrate
        {
            get
            {
                return myGoal.Carbohydrate;
            }
            set
            {
                if (value != null)
                    myGoal.Carbohydrate = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Fat
        {
            get
            {
                return myGoal.Fat;
            }
            set
            {
                if (value != null)
                    myGoal.Fat = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Protein
        {
            get
            {
                return myGoal.Protein;
            }
            set
            {
                if (value != null)
                    myGoal.Protein = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Sodium
        {
            get
            {
                return myGoal.Sodium;
            }
            set
            {
                if (value != null)
                    myGoal.Sodium = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }

        public double? Sugar
        {
            get
            {
                return myGoal.Sugar;
            }
            set
            {
                if (value != null)
                    myGoal.Sugar = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)SaveGoalCommand).RaiseCanExecuteChanged();
            }
        }


        private bool CanSaveGoal(Type arg)
        {
            if (Calories < 0 || Calories == null ||
                Carbohydrate < 0 || Carbohydrate == null ||
                Fat < 0 || Fat == null ||
                Protein < 0 || Protein == null ||
                Sodium < 0 || Sodium == null ||
                Sugar < 0 || Sugar == null )
                return false;
            return true;
        }

        private async void RunSaveGoal(Type obj)
        {
            await SaveGoal();
        }

        private async Task SaveGoal()
        {
            try
            {
                Goal temp = new Goal();
                temp.Date = myGoal.Date;
                temp.Calories = myGoal.Calories;
                temp.Carbohydrate = myGoal.Carbohydrate;
                temp.Fat = myGoal.Fat;
                temp.Protein = myGoal.Protein;
                temp.Sodium = myGoal.Sodium;
                temp.Sugar = myGoal.Sugar;
                // myGoal = temp;

                await myBl.AddGoal(temp);
                await _myMessageDialog.ShowInfoDialogAsync("Goal Saved!");
                OnPropertyChanged();
            }
            catch(Exception ex)
            {
                updateMyGoal();
                await _myMessageDialog.ShowInfoDialogAsync(ex.Message);
            }
        }

        public async Task updateMyGoal()
        {
            if (myGoal == null)
                myGoal = new Goal();
            var tempGoal = await myBl.GetGoal(myGoal.Date);
            if (tempGoal != null)
                myGoal = tempGoal;
            OnPropertyChanged("Calories");
            OnPropertyChanged("Carbohydrate");
            OnPropertyChanged("Fat");
            OnPropertyChanged("Protein");
            OnPropertyChanged("Sodium");
            OnPropertyChanged("Sugar");
        }
        

        //////////////////////////////////////////// Commands:
        public ICommand SaveGoalCommand { get; }
    }
}
