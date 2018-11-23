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

            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyAddFoodCommand = new DelegateCommand<Type>(RunOpenAddFood, CanOpen);
            OpenMyGoalsCommand = new DelegateCommand<Type>(RunOpenGoals, CanOpen);
            OpenMyProfilCommand = new DelegateCommand<Type>(RunOpenProfil, CanOpen);
            OpenMyStatisticCommand = new DelegateCommand<Type>(RunOpenStatistic, CanOpen);
            OpenMyLogoutCommand = new DelegateCommand<Type>(RunMakeLogout, CanOpen);

            


            HomeView = new HomeView();          

            SelectedView = HomeView;

            init();

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
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            SelectedView = HomeView;
        }

        private void RunOpenAddFood(Type obj)
        {
            _eventAggregator.GetEvent<OpenAddFoodEvent>().Publish();
            if (AddFoodView == null)
                AddFoodView = new AddFoodView();
            SelectedView = AddFoodView;
        }
        private void RunOpenGoals(Type obj)
        {
            _eventAggregator.GetEvent<OpenGoalsEvent>().Publish();
            if (GoalsView == null)
                GoalsView = new GoalsView();
            SelectedView = GoalsView;
        }
        private void RunOpenProfil(Type obj)
        {
            _eventAggregator.GetEvent<OpenProfilEvent>().Publish();
            if (ProfilView == null)
                ProfilView = new ProfilView();
            SelectedView = ProfilView;
        }
        private void RunOpenStatistic(Type obj)
        {
            _eventAggregator.GetEvent<OpenStatisticEvent>().Publish();
            if (StatisticView == null)
                StatisticView = new StatisticView();
            SelectedView = StatisticView;
        }
        private void RunMakeLogout(Type obj)
        {
            _eventAggregator.GetEvent<LogoutEvent>().Publish();
            SelectedView = null;
        }

        private void OpenHome()
        {
           SelectedView = HomeView;
        }
        private void OpenAddFood()
        {
            if (AddFoodView == null)
                AddFoodView = new AddFoodView();
            SelectedView = AddFoodView;
        }
        private void OpenGoals()
        {
            if (GoalsView == null)
                GoalsView = new GoalsView();
            SelectedView = GoalsView;
        }
        private void OpenProfil()
        {
            if (ProfilView == null)
                ProfilView = new ProfilView();
            SelectedView = ProfilView;
        }
        private void OpenStatistic()
        {
            if (StatisticView == null)
                StatisticView = new StatisticView();
            SelectedView = StatisticView;
        }
        private void MakeLogout()
        {
            SelectedView = null;
        }


        private async Task init()
        {

           await myBl.AddUser(new BE.Entitys.User("myMail"));
           await myBl.SetCurrentUser("myMail");

        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyHomeCommand { get; set; }
        public ICommand OpenMyGoalsCommand { get; set; }
        public ICommand OpenMyAddFoodCommand { get; set; }
        public ICommand OpenMyStatisticCommand { get; set; }
        public ICommand OpenMyProfilCommand { get; set; }
        public ICommand OpenMyLogoutCommand { get; set; }
        

    }


}
