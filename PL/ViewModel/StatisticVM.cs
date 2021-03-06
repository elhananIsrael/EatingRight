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
using BE.Entitys;
using BL;
using PL.Tools;

namespace PL.ViewModel
{
    class StatisticVM:BaseVM
    {
        public StatisticVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {

            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
            myBl = new Bl();           
        }


        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;
        public Bl myBl;

        private Goal myPercentageGoal;

        public string Calories {
            get { return (myPercentageGoal.Calories * 100).ToString() + "%"; }
            set { }
        }
        public string Carbohydrate { get; set; }
        public string Fat { get; set; }
        public string Protein { get; set; }
        public string Sodium { get; set; }
        public string Sugar { get; set; }


        public Goal MyPercentageGoal
        {
            get {
                return myPercentageGoal;
            }
            set
            {
                if (value != null)
                {                   
                    myPercentageGoal = new Goal();
                    myPercentageGoal = value;
                    updateMyPercentageGoal();
                    OnPropertyChanged();
                }
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                if (myPercentageGoal == null)
                {
                    updateMyPercentageGoal();
                    selectedDate = myPercentageGoal.Date;
                }
                return selectedDate;
            }
            set
            {

                if (value != null && value != myPercentageGoal.Date)
                {

                    myPercentageGoal = new Goal();
                    myPercentageGoal.Date = value;
                    selectedDate = value;
                    updateMyPercentageGoal();
                    //OnPropertyChanged();
                }
            }
        }



        public async Task updateMyPercentageGoal()
        {
          
            if (myPercentageGoal == null)
                myPercentageGoal = new Goal();
            var tempGoal = await myBl.GetPercentageOfTheMealFromGoal(myPercentageGoal.Date);
            if (tempGoal != null)
                myPercentageGoal = tempGoal;
            OnPropertyChanged("MyPercentageGoal");
        }

    
    }
}





       