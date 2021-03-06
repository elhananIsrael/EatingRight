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
using BL;
using BE.Entitys;
using PL.Tools;

namespace PL.ViewModel
{
    class ProfilVM:BaseVM
    {
        public ProfilVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
            myBl = new Bl();          

            updateMyCurrentUser();
        }
       

        public Bl myBl;
        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;

        private User myCurrentUser;

        public User MyCurrentUser {
            get { return myCurrentUser; }
            set {
                if (value != null)
                    myCurrentUser = value;
                OnPropertyChanged("MyCurrentUser");
                OnPropertyChanged("MyUserBirth");
            }
            
        }

        public string MyUserBirth
        {
            get { return (myCurrentUser.BirthDate.Day.ToString() + "/"+myCurrentUser.BirthDate.Month.ToString() + "/"+ myCurrentUser.BirthDate.Year.ToString() ); }
            set { ; }
        }

        public async Task updateMyCurrentUser()
        {
            if (myCurrentUser == null)
                myCurrentUser = new User();
            var tempUser = await myBl.GetCurrentUser();
            if (tempUser != null)
                MyCurrentUser = tempUser;
        }



        
    }
}

      