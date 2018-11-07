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

        public MainWindowVM()
        {
            _eventAggregator = new Prism.Events.EventAggregator();
            _eventAggregator.GetEvent<PL.Events.OpenHomeEvent>()
               .Subscribe(OpenHome);
            HomeView = new HomeView();
           // SelectedView = HomeView;
            OpenMyHomeCommand = new DelegateCommand<Type>(OnOpenHome, CanOpenHome);

        }

       
        public PL.View.HomeView HomeView { get; }

        private object selectedView;
        
        public object SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; OnPropertyChanged(); }
        }


        //////////////////////////////////////////// Function:

        private void OpenHome()
        {
            //RegisterMode = false;
            SelectedView = HomeView;
        }


        private bool CanOpenHome(Type obj)
        {
            return true;
        }

        private void OnOpenHome(Type obj)
        {
           //  _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
            SelectedView = HomeView;
        }

        public ICommand OpenMyHomeCommand { get; }



    }

    
}
