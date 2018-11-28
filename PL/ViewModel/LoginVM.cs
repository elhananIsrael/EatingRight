﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using Prism.Commands;
using BL;
using PL.Events;
using BE.Entitys;


namespace PL.ViewModel
{
    class LoginVM:BaseVM
    {

        public LoginVM()
        {

            myBl = new Bl();
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyRegistrationCommand = new DelegateCommand<Type>(RunOpenRegistration, CanOpen);
        }
        Bl myBl;

        private IEventAggregator _eventAggregator;

        public string FirstName { get; set; }
        public string Password { get; set; }




        //////////////////////////////////////////// Function:

        //////////////////////////// Command Function:
        private bool CanOpen(Type obj)
        {
            // if(selectedView!=LoginView && selectedView!=RegistrationView)
            return true;
            //  return false;
        }

        private void RunOpenRegistration(Type obj)
        {
            _eventAggregator.GetEvent<OpenRegistrationEvent>().Publish();
           
        }

        private void RunOpenHome(Type obj)
        {
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();            
        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyRegistrationCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
    }
}
