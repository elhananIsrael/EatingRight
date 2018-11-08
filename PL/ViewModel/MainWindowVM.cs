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

namespace PL.ViewModel
{   

    public class MainWindowVM: BaseVM
    {
        private IEventAggregator _eventAggregator;

        public PL.View.HomeView HomeView { get; }
        public PL.View.AddFoodView AddFoodView { get; }
        public PL.View.GoalsView GoalsView { get; }
        public PL.View.ProfilView ProfilView { get; }
        public PL.View.StatisticView StatisticView { get; }


        private object selectedView;

        public MainWindowVM()
        {
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
           

            HomeView = new HomeView();
            AddFoodView = new AddFoodView();
            GoalsView = new GoalsView();
            ProfilView = new ProfilView();
            StatisticView = new StatisticView();

            SelectedView = HomeView;

            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyAddFoodCommand = new DelegateCommand<Type>(RunOpenAddFood, CanOpen);
            OpenMyGoalsCommand = new DelegateCommand<Type>(RunOpenGoals, CanOpen);
            OpenMyProfilCommand = new DelegateCommand<Type>(RunOpenProfil, CanOpen);
            OpenMyStatisticCommand = new DelegateCommand<Type>(RunOpenStatistic, CanOpen);
            OpenMyLogoutCommand = new DelegateCommand<Type>(RunMakeLogout, CanOpen);


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
            SelectedView = AddFoodView;
        }
        private void RunOpenGoals(Type obj)
        {
            _eventAggregator.GetEvent<OpenGoalsEvent>().Publish();
            SelectedView = GoalsView;
        }
        private void RunOpenProfil(Type obj)
        {
            _eventAggregator.GetEvent<OpenProfilEvent>().Publish();
            SelectedView = ProfilView;
        }
        private void RunOpenStatistic(Type obj)
        {
            _eventAggregator.GetEvent<OpenStatisticEvent>().Publish();
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
            SelectedView = AddFoodView;
        }
        private void OpenGoals()
        {
            SelectedView = GoalsView;
        }
        private void OpenProfil()
        {
            SelectedView = ProfilView;
        }
        private void OpenStatistic()
        {
            SelectedView = StatisticView;
        }
        private void MakeLogout()
        {
            SelectedView = null;
        }






        //////////////////////////////////////////// Commands:
        public ICommand OpenMyHomeCommand { get; }
        public ICommand OpenMyGoalsCommand { get; }
        public ICommand OpenMyAddFoodCommand { get; }
        public ICommand OpenMyStatisticCommand { get; }
        public ICommand OpenMyProfilCommand { get; }
        public ICommand OpenMyLogoutCommand { get; }
        

    }


}
