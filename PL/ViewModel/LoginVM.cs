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
    class LoginVM:BaseVM
    {

        public LoginVM(IEventAggregator eventAggregator, IMyMessageDialog myMessageDialog)
        {
            _eventAggregator = eventAggregator;
            _myMessageDialog = myMessageDialog;
            myBl = new Bl();
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpenHome);
            OpenMyRegistrationCommand = new DelegateCommand<Type>(RunOpenRegistration, CanOpenRegistration);
        }
        Bl myBl;

        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;

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
            if (!string.IsNullOrEmpty(Email) && myBl.IsValidMailAddress(Email) && !string.IsNullOrEmpty(Password))
                return true;
            else return false;
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
            try{ 
            var user = await myBl.GetUserByEmail(email);
                
                if (user == null)
                    await _myMessageDialog.ShowInfoDialogAsync("Invalid User Email Or Wrong Password!");
                else
                {
                    var loginEmail = email.ToLower();
                    var userEmail = user.Email.ToLower();
                    if (user != null && loginEmail == userEmail && user.Password == password)
                    {
                        await myBl.SetCurrentUser(user.Email);
                        _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
                    }
                    else if (loginEmail != userEmail || user.Password != password)
                        await _myMessageDialog.ShowInfoDialogAsync("Invalid User Email Or Wrong Password!");

                }
            }
            catch (Exception e)
            {
                await _myMessageDialog.ShowInfoDialogAsync(e.Message);
            }
        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyRegistrationCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
    }
}
