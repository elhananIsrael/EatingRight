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
using PL.Tools;

namespace PL.ViewModel
{   

    public class MainWindowVM: BaseVM
    {
        Bl myBl;

        private IEventAggregator _eventAggregator;
        private IMyMessageDialog _myMessageDialog;


        public PL.View.LoginView LoginView { get; set; }
        public PL.View.RegistrationView RegistrationView { get; set; }
        public PL.View.HomeView HomeView { get; set; }
        public PL.View.AddFoodView AddFoodView { get; set; }
        public PL.View.GoalsView GoalsView { get; set; }
        public PL.View.ProfilView ProfilView { get; set; }
        public PL.View.StatisticView StatisticView { get; set; }
        public PL.View.BodyMeasurementsView BodyMeasurementsView { get; set; }

        private object selectedView;
        bool isLoginNow = false;

        public MainWindowVM()
        {
            myBl = new Bl();

            _eventAggregator = new Prism.Events.EventAggregator();
            _myMessageDialog = new MyMessageDialog();
           
            _eventAggregator.GetEvent<PL.Events.OpenLoginEvent>()
              .Subscribe(OpenLogin);
            _eventAggregator.GetEvent<PL.Events.OpenRegistrationEvent>()
              .Subscribe(OpenRegistration);
            _eventAggregator.GetEvent<PL.Events.OpenHomeEvent>()
               .Subscribe(OpenHome);             
        
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyAddFoodCommand = new DelegateCommand<Type>(RunOpenAddFood, CanOpen);
            OpenMyGoalsCommand = new DelegateCommand<Type>(RunOpenGoals, CanOpen);
            OpenMyBodyMeasurementsCommand = new DelegateCommand<Type>(RunOpenBodyMeasurements, CanOpen);
            OpenMyProfilCommand = new DelegateCommand<Type>(RunOpenProfil, CanOpen);
            OpenMyStatisticCommand = new DelegateCommand<Type>(RunOpenStatistic, CanOpen);
            OpenMyLogoutCommand = new DelegateCommand<Type>(RunMakeLogout, CanOpen);



            LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            HomeView = new HomeView();          

            SelectedView = LoginView;     

        }


        
        private bool isLogoutSelected = true;
        public bool IsLogoutSelected
        {
            get
            {
                return isLogoutSelected;
            }
            set
            {
                isLogoutSelected = value;
                OnPropertyChanged();
            }
        }
        private bool isHomeSelected = false;
        public bool IsHomeSelected
        {
            get
            {
                return isHomeSelected;
            }
            set
            {
                isHomeSelected = value;
                OnPropertyChanged();
            }
        }
        private bool isHamburgerMenuEnable = false;
        public bool IsHamburgerMenuEnable
        {
            get
            {
                return isHamburgerMenuEnable;
            }
            set
            {
                isHamburgerMenuEnable = value;
                OnPropertyChanged();
            }
        }
        

        public object SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; OnPropertyChanged(); }
        }


        //////////////////////////////////////////// Function:

        //////////////////////////// Command Function:
        private bool CanOpen(Type obj)
        {
            return true;
        }


        private void RunOpenHome(Type obj)
        {
            OpenHome();
        }

        private void RunOpenAddFood(Type obj)
        {
            AddFoodView = new AddFoodView(_eventAggregator, _myMessageDialog);
            SelectedView = AddFoodView;
            IsHomeSelected = false;
        }
        private void RunOpenGoals(Type obj)
        {
            GoalsView = new GoalsView(_eventAggregator, _myMessageDialog);
            SelectedView = GoalsView;
            IsHomeSelected = false; 
        }


        private void RunOpenBodyMeasurements(Type obj)
        {
            BodyMeasurementsView = new BodyMeasurementsView(_eventAggregator, _myMessageDialog);
            SelectedView = BodyMeasurementsView;
            IsHomeSelected = false; 
        }

        private void RunOpenProfil(Type obj)
        {
            ProfilView = new ProfilView(_eventAggregator, _myMessageDialog);
            SelectedView = ProfilView;
            IsHomeSelected = false;            
        }
        private void RunOpenStatistic(Type obj)
        {
            StatisticView = new StatisticView(_eventAggregator, _myMessageDialog);
            SelectedView = StatisticView;
            IsHomeSelected = false;
        }
        private void RunMakeLogout(Type obj)
        {
            IsHamburgerMenuEnable = false;
            LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            isLoginNow = false;
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;

        }



        private void OpenLogin()
        {
            if (IsHamburgerMenuEnable != false)
                IsHamburgerMenuEnable = false;
                LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }
        private void OpenRegistration()
        {
            RegistrationView = new RegistrationView(_eventAggregator, _myMessageDialog);
            SelectedView = RegistrationView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }

        private async void OpenHome()
        {
            if (IsHamburgerMenuEnable == false)
                IsHamburgerMenuEnable = true;
            if (HomeView == null)
                HomeView = new HomeView();
            SelectedView = HomeView;
            IsLogoutSelected = false;
            IsHomeSelected = true;
            if(!isLoginNow)
            {
                isLoginNow = true;
                var user = await myBl.GetCurrentUser();
                await _myMessageDialog.ShowInfoDialogAsync("Welcome "+ user.FirstName + " " +user.LastName+"!");
            }
        }


        //////////////////////////////////////////// Commands:
        public ICommand OpenMyLoginCommand { get; set; }
        public ICommand OpenMyRegistrationCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
        public ICommand OpenMyGoalsCommand { get; set; }
        public ICommand OpenMyAddFoodCommand { get; set; }
        public ICommand OpenMyStatisticCommand { get; set; }
        public ICommand OpenMyBodyMeasurementsCommand { get; set; }
        public ICommand OpenMyProfilCommand { get; set; }
        public ICommand OpenMyLogoutCommand { get; set; }

    }


}
