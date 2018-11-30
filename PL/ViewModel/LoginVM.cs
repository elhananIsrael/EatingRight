using System;
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

        public LoginVM(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            myBl = new Bl();
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpenHome);
            OpenMyRegistrationCommand = new DelegateCommand<Type>(RunOpenRegistration, CanOpenRegistration);
        }
        Bl myBl;

        private IEventAggregator _eventAggregator;

        private string email = "";
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }




        //////////////////////////////////////////// Function:

        //////////////////////////// Command Function:
        private bool CanOpenHome(Type obj)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                return true;
            else return false;

            //  return false;
        }

        private bool CanOpenRegistration(Type obj)
        {
            return true;
        }

        private void RunOpenRegistration(Type obj)
        {
            _eventAggregator.GetEvent<OpenRegistrationEvent>().Publish();
           
        }

        private async void RunOpenHome(Type obj)
        {
            var user = await myBl.GetUserByEmail(email);
            if (user != null && user.Email == Email && user.Password == password)
            {
                await myBl.SetCurrentUser(Email);
                _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
                _eventAggregator.GetEvent<UpdateUserEvent>().Publish();
            }
        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyRegistrationCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
    }
}
