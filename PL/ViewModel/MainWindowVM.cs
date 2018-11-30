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

namespace PL.ViewModel
{   

    public class MainWindowVM: BaseVM
    {
        Bl myBl;

        private IEventAggregator _eventAggregator;

        public PL.View.LoginView LoginView { get; set; }
        public PL.View.RegistrationView RegistrationView { get; set; }
        public PL.View.HomeView HomeView { get; set; }
        public PL.View.AddFoodView AddFoodView { get; set; }
        public PL.View.GoalsView GoalsView { get; set; }
        public PL.View.ProfilView ProfilView { get; set; }
        public PL.View.StatisticView StatisticView { get; set; }

        private object selectedView;

        public MainWindowVM()
        {
            myBl = new Bl();

            _eventAggregator = new Prism.Events.EventAggregator();

            _eventAggregator.GetEvent<PL.Events.UpdateUserEvent>()
             .Subscribe(updateDetails);
            _eventAggregator.GetEvent<PL.Events.OpenLoginEvent>()
              .Subscribe(OpenLogin);
            _eventAggregator.GetEvent<PL.Events.OpenRegistrationEvent>()
              .Subscribe(OpenRegistration);
            _eventAggregator.GetEvent<PL.Events.OpenHomeEvent>()
               .Subscribe(OpenHome);
            _eventAggregator.GetEvent<PL.Events.OpenAddFoodEvent>()
               .Subscribe(OpenAddFood);
            _eventAggregator.GetEvent<PL.Events.OpenGoalsEvent>()
               .Subscribe(OpenGoals);
            _eventAggregator.GetEvent<PL.Events.OpenProfilEvent>()
               .Subscribe(OpenProfil);
            _eventAggregator.GetEvent<PL.Events.OpenStatisticEvent>()
               .Subscribe(OpenStatistic);
            _eventAggregator.GetEvent<PL.Events.LogoutEvent>()
               .Subscribe(MakeLogout);


            
            OpenMyLoginCommand = new DelegateCommand<Type>(RunOpenLogin, CanOpen);
            OpenMyRegistrationCommand = new DelegateCommand<Type>(RunOpenRegistration, CanOpen);
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyAddFoodCommand = new DelegateCommand<Type>(RunOpenAddFood, CanOpen);
            OpenMyGoalsCommand = new DelegateCommand<Type>(RunOpenGoals, CanOpen);
            OpenMyProfilCommand = new DelegateCommand<Type>(RunOpenProfil, CanOpen);
            OpenMyStatisticCommand = new DelegateCommand<Type>(RunOpenStatistic, CanOpen);
            OpenMyLogoutCommand = new DelegateCommand<Type>(RunMakeLogout, CanOpen);



            LoginView = new LoginView(_eventAggregator);
            HomeView = new HomeView();          

            SelectedView = LoginView;
            
            

            init();

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
           // if(selectedView!=LoginView && selectedView!=RegistrationView)
            return true;
          //  return false;
        }


        private void RunOpenLogin(Type obj)
        {
            _eventAggregator.GetEvent<OpenLoginEvent>().Publish();
            if (LoginView == null)
                LoginView = new LoginView(_eventAggregator);
            SelectedView = LoginView;
        }

        private void RunOpenRegistration(Type obj)
        {
            _eventAggregator.GetEvent<OpenRegistrationEvent>().Publish();
            if (RegistrationView == null)
                RegistrationView = new RegistrationView(_eventAggregator);
            SelectedView = RegistrationView;
        }

        private void RunOpenHome(Type obj)
        {
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            if (IsHamburgerMenuEnable == false)
                IsHamburgerMenuEnable = true;
            if (HomeView == null)
                HomeView = new HomeView();
            SelectedView = HomeView;
        }

        private void RunOpenAddFood(Type obj)
        {
            _eventAggregator.GetEvent<OpenAddFoodEvent>().Publish();
            if (AddFoodView == null)
                AddFoodView = new AddFoodView(_eventAggregator);
            SelectedView = AddFoodView;
        }
        private void RunOpenGoals(Type obj)
        {
            _eventAggregator.GetEvent<OpenGoalsEvent>().Publish();
            if (GoalsView == null)
                GoalsView = new GoalsView(_eventAggregator);
            SelectedView = GoalsView;
        }
        private void RunOpenProfil(Type obj)
        {
            _eventAggregator.GetEvent<OpenProfilEvent>().Publish();
            if (ProfilView == null)
                ProfilView = new ProfilView(_eventAggregator);
            SelectedView = ProfilView;
        }
        private void RunOpenStatistic(Type obj)
        {
            _eventAggregator.GetEvent<OpenStatisticEvent>().Publish();
            if (StatisticView == null)
                StatisticView = new StatisticView(_eventAggregator);
            SelectedView = StatisticView;
        }
        private void RunMakeLogout(Type obj)
        {
            _eventAggregator.GetEvent<LogoutEvent>().Publish();
            IsHamburgerMenuEnable = false;
            SelectedView = LoginView;
        }



        private void OpenLogin()
        {
            if (IsHamburgerMenuEnable != false)
                IsHamburgerMenuEnable = false;
            if (LoginView == null)
                LoginView = new LoginView(_eventAggregator);
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }
        private void OpenRegistration()
        {
            if (RegistrationView == null)
                RegistrationView = new RegistrationView(_eventAggregator);
            SelectedView = RegistrationView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }

        private void OpenHome()
        {
            if (IsHamburgerMenuEnable == false)
                IsHamburgerMenuEnable = true;
            if (HomeView == null)
                HomeView = new HomeView();
            SelectedView = HomeView;
            IsLogoutSelected = false;
            IsHomeSelected = true;
            
        }
        private void OpenAddFood()
        {
            if (AddFoodView == null)
                AddFoodView = new AddFoodView(_eventAggregator);
            SelectedView = AddFoodView;
            IsHomeSelected = false;
        }
        private void OpenGoals()
        {
            if (GoalsView == null)
                GoalsView = new GoalsView(_eventAggregator);
            SelectedView = GoalsView;
            IsHomeSelected = false;
        }
        private void OpenProfil()
        {
            if (ProfilView == null)
                ProfilView = new ProfilView(_eventAggregator);
            SelectedView = ProfilView;
            IsHomeSelected = false;
        }
        private void OpenStatistic()
        {
            if (StatisticView == null)
                StatisticView = new StatisticView(_eventAggregator);
            SelectedView = StatisticView;
            IsHomeSelected = false;
        }
        private void MakeLogout()
        {
            IsHamburgerMenuEnable = false;
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }


        private async Task init()
        {          

        }


       public void updateDetails()
        {

        }


        //////////////////////////////////////////// Commands:
        public ICommand OpenMyLoginCommand { get; set; }
        public ICommand OpenMyRegistrationCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
        public ICommand OpenMyGoalsCommand { get; set; }
        public ICommand OpenMyAddFoodCommand { get; set; }
        public ICommand OpenMyStatisticCommand { get; set; }
        public ICommand OpenMyProfilCommand { get; set; }
        public ICommand OpenMyLogoutCommand { get; set; }
        

    }


}
