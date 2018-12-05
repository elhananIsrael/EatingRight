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
using PL.Tools;

namespace PL.ViewModel
{
    class RegistrationVM : BaseVM
    {

        public RegistrationVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
            myBl = new Bl();
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpenHome);
            OpenMyLoginCommand = new DelegateCommand<Type>(RunOpenLogin, CanOpenLogin);
        }

        Bl myBl;

        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;

        private User regUser;

        public string FirstName
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.FirstName;
            }
            set
            {
                regUser.FirstName = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }

        public string LastName
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.LastName;
            }
            set
            {
                regUser.LastName = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }


        public string Email
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.Email;
            }
            set
            {
                regUser.Email = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }

        public string Password
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.Password;
            }
            set
            {
                regUser.Password = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }

        public DateTime BirthDate
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.BirthDate;
            }
            set
            {
                regUser.BirthDate = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }

      /*  public double? Weight
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser.Weight;
            }
            set
            {
                regUser.Weight = value;
                OnPropertyChanged();
                ((DelegateCommand<Type>)OpenMyHomeCommand).RaiseCanExecuteChanged();

            }
        }
        */

        //////////////////////////////////////////// Function:

        //////////////////////////// Command Function:
        private bool CanOpenHome(Type obj)
        {
            if (!string.IsNullOrEmpty(regUser.Email) &&  myBl.IsValidMailAddress(regUser.Email) && !string.IsNullOrEmpty(regUser.Password) &&
                !string.IsNullOrEmpty(regUser.FirstName) && !string.IsNullOrEmpty(regUser.LastName) &&
               /* regUser.BodyMeasurements.Weight!=null && regUser.Weight > 0  &&*/ regUser.BirthDate != null && regUser.BirthDate < DateTime.Now)
                return true;
            else return false;
        }


        private bool CanOpenLogin(Type obj)
        {
            return true;
        }

        private void RunOpenLogin(Type obj)
        {
          
            _eventAggregator.GetEvent<OpenLoginEvent>().Publish();

        }

        private async void RunOpenHome(Type obj)
        {
            await myBl.AddUser(regUser);
            await myBl.SetCurrentUser(regUser.Email);
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            _eventAggregator.GetEvent<UpdateUserEvent>().Publish();
        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyLoginCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
    }
}
