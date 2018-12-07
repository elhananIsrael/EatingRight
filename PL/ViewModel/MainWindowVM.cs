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

            _eventAggregator.GetEvent<PL.Events.UpdateUserEvent>()
             .Subscribe(updateDetails);
            _eventAggregator.GetEvent<PL.Events.OpenLoginEvent>()
              .Subscribe(OpenLogin);
            _eventAggregator.GetEvent<PL.Events.OpenRegistrationEvent>()
              .Subscribe(OpenRegistration);
            _eventAggregator.GetEvent<PL.Events.OpenHomeEvent>()
               .Subscribe(OpenHome);
          /*  _eventAggregator.GetEvent<PL.Events.OpenAddFoodEvent>()
               .Subscribe(OpenAddFood);
            _eventAggregator.GetEvent<PL.Events.OpenGoalsEvent>()
               .Subscribe(OpenGoals);
            _eventAggregator.GetEvent<PL.Events.OpenBodyMeasurementsEvent>()
               .Subscribe(OpenBodyMeasurements);
            _eventAggregator.GetEvent<PL.Events.OpenProfilEvent>()
               .Subscribe(OpenProfil);
            _eventAggregator.GetEvent<PL.Events.OpenStatisticEvent>()
               .Subscribe(OpenStatistic);
            _eventAggregator.GetEvent<PL.Events.LogoutEvent>()
               .Subscribe(MakeLogout);*/


            
          //  OpenMyLoginCommand = new DelegateCommand<Type>(RunOpenLogin, CanOpen);
          //  OpenMyRegistrationCommand = new DelegateCommand<Type>(RunOpenRegistration, CanOpen);
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


      /*  private void RunOpenLogin(Type obj)
        {
            _eventAggregator.GetEvent<OpenLoginEvent>().Publish();
                LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            SelectedView = LoginView;
        }*/

      /*  private void RunOpenRegistration(Type obj)
        {
            _eventAggregator.GetEvent<OpenRegistrationEvent>().Publish();
                RegistrationView = new RegistrationView(_eventAggregator, _myMessageDialog);
            SelectedView = RegistrationView;
        }*/

        private void RunOpenHome(Type obj)
        {
            OpenHome();
            /*  _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            if (IsHamburgerMenuEnable == false)
                IsHamburgerMenuEnable = true;
            if (HomeView == null)
                HomeView = new HomeView();
            SelectedView = HomeView;*/
        }

        private void RunOpenAddFood(Type obj)
        {
            AddFoodView = new AddFoodView(_eventAggregator, _myMessageDialog);
            SelectedView = AddFoodView;
            IsHomeSelected = false;
            /* _eventAggregator.GetEvent<OpenAddFoodEvent>().Publish();
                AddFoodView = new AddFoodView(_eventAggregator, _myMessageDialog);
            SelectedView = AddFoodView;*/
        }
        private void RunOpenGoals(Type obj)
        {
            GoalsView = new GoalsView(_eventAggregator, _myMessageDialog);
            SelectedView = GoalsView;
            IsHomeSelected = false;         
            /* _eventAggregator.GetEvent<OpenGoalsEvent>().Publish();
                GoalsView = new GoalsView(_eventAggregator, _myMessageDialog);
            SelectedView = GoalsView;*/
        }


        private void RunOpenBodyMeasurements(Type obj)
        {
            BodyMeasurementsView = new BodyMeasurementsView(_eventAggregator, _myMessageDialog);
            SelectedView = BodyMeasurementsView;
            IsHomeSelected = false;  
            /* _eventAggregator.GetEvent<OpenBodyMeasurementsEvent>().Publish();
            BodyMeasurementsView = new BodyMeasurementsView(_eventAggregator, _myMessageDialog);
            SelectedView = BodyMeasurementsView;*/
        }

        private void RunOpenProfil(Type obj)
        {
            ProfilView = new ProfilView(_eventAggregator, _myMessageDialog);
            SelectedView = ProfilView;
            IsHomeSelected = false;            /* _eventAggregator.GetEvent<OpenProfilEvent>().Publish();
                ProfilView = new ProfilView(_eventAggregator, _myMessageDialog);
            SelectedView = ProfilView;*/
        }
        private void RunOpenStatistic(Type obj)
        {
            StatisticView = new StatisticView(_eventAggregator, _myMessageDialog);
            SelectedView = StatisticView;
            IsHomeSelected = false;
            /* _eventAggregator.GetEvent<OpenStatisticEvent>().Publish();
                StatisticView = new StatisticView(_eventAggregator, _myMessageDialog);
            SelectedView = StatisticView;*/
        }
        private void RunMakeLogout(Type obj)
        {
            IsHamburgerMenuEnable = false;
            LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            isLoginNow = false;
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
            /*_eventAggregator.GetEvent<LogoutEvent>().Publish();
            IsHamburgerMenuEnable = false;
            LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            SelectedView = LoginView;*/
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
      /*  private void OpenAddFood()
        {
            AddFoodView = new AddFoodView(_eventAggregator, _myMessageDialog);
            SelectedView = AddFoodView;
            IsHomeSelected = false;
        }*/
      /*  private void OpenGoals()
        {
            GoalsView = new GoalsView(_eventAggregator, _myMessageDialog);
            SelectedView = GoalsView;
            IsHomeSelected = false;
        }*/

       /* private void OpenBodyMeasurements()
        {
            BodyMeasurementsView = new BodyMeasurementsView(_eventAggregator, _myMessageDialog);
            SelectedView = BodyMeasurementsView;
            IsHomeSelected = false;
        }*/
        
    /*    private void OpenProfil()
        {
            ProfilView = new ProfilView(_eventAggregator, _myMessageDialog);
            SelectedView = ProfilView;
            IsHomeSelected = false;
        }
        private void OpenStatistic()
        {
            StatisticView = new StatisticView(_eventAggregator, _myMessageDialog);
            SelectedView = StatisticView;
            IsHomeSelected = false;
        }
        private void MakeLogout()
        {
            IsHamburgerMenuEnable = false;
            LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            isLoginNow = false;
            SelectedView = LoginView;
            IsHomeSelected = false;
            IsLogoutSelected = true;
        }*/


        private async Task init()
        {          

        }


       public void updateDetails()
        {
           /* //LoginView = new LoginView(_eventAggregator, _myMessageDialog);
            RegistrationView =new RegistrationView(_eventAggregator, _myMessageDialog);
            // HomeView=new HomeView(_eventAggregator, _myMessageDialog);
            AddFoodView =new AddFoodView(_eventAggregator, _myMessageDialog);
            GoalsView =new GoalsView(_eventAggregator, _myMessageDialog);
            ProfilView =new ProfilView(_eventAggregator, _myMessageDialog);
            StatisticView =new StatisticView(_eventAggregator, _myMessageDialog);*/
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
