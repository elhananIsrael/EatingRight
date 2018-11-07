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

        private object selectedView;

        public MainWindowVM()
        {
            _eventAggregator = new Prism.Events.EventAggregator();
            _eventAggregator.GetEvent<PL.Events.OpenHomeEvent>()
               .Subscribe(OpenHome);
            HomeView = new HomeView();
           // SelectedView = HomeView;
            OpenMyHomeCommand = new DelegateCommand<Type>(OnOpenHome, CanOpenHome);

        }

       

        
        public object SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; OnPropertyChanged(); }
        }


        //////////////////////////////////////////// Function:

        private void OpenHome()
        {
            SelectedView = HomeView;
        }


        private bool CanOpenHome(Type obj)
        {
            return true;
        }

        private void OnOpenHome(Type obj)
        {
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            SelectedView = HomeView;
        }

        //////////////////////////////////////////// Commands:
        public ICommand OpenMyHomeCommand { get; }
        public ICommand OpenMyGoalCommand { get; }
        public ICommand OpenMyAdFoodCommand { get; }
        public ICommand OpenMyStatisticCommand { get; }
        public ICommand OpenMyProfilCommand { get; }
        public ICommand OpenMyLogoutCommand { get; }
        

    }


}
