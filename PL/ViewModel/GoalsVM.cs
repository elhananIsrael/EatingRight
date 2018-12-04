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
            _eventAggregator.GetEvent<PL.Events.UpdateUserEvent>()
              .Subscribe(updateDetails);

            myBl = new Bl();
            updateMyGoal();

            SaveGoalCommand = new DelegateCommand<Type>(RunSaveGoal, CanSaveGoal);           
        }

        public Bl myBl;
        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;
        //private IMessageDialogService _messageDialogService;

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
                if (value != null)
                {
                    MyGoal = new Goal();
                    MyGoal.Date = value;
                    updateMyGoal();
                    //updateMyFoodToday();
                    OnPropertyChanged();
                }
            }
        }

        public Goal MyGoal
        {
            get
            {
                if (myGoal == null)
                {
                    updateMyGoal();
                    // updateMyFoodToday();
                }
                return myGoal;
            }
            set
            {
                if (value != null)
                    myGoal = value;

                OnPropertyChanged();
            }
        }



        private bool CanSaveGoal(Type arg)
        {
            /*  if (SelectedSearchFood != null && SelectedSearchFood.Name.Length > 0)
                  return true;
              return false;*/
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
                // await messagedialoge.ShowInfoDialogAsync("Error The Goal not Save Please contact the administrator");
                updateMyGoal();
                await _myMessageDialog.ShowInfoDialogAsync(ex.Message);
            }
        }

        public async Task updateMyGoal()
        {
            if (myGoal == null)
                MyGoal = new Goal();
            var tempGoal = await myBl.GetGoal(myGoal.Date);
            if (tempGoal != null)
                MyGoal = tempGoal;
        }

        public async void updateDetails()
        {
            //await updateMyGoal();
        }

        //////////////////////////////////////////// Commands:
        public ICommand SaveGoalCommand { get; }
    }
}
