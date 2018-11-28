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
    class RegistrationVM : BaseVM
    {

        public RegistrationVM()
        {
            myBl = new Bl();
            OpenMyHomeCommand = new DelegateCommand<Type>(RunOpenHome, CanOpen);
            OpenMyLoginCommand = new DelegateCommand<Type>(RunOpenLogin, CanOpen);
        }

        Bl myBl;

        private IEventAggregator _eventAggregator;
        private User regUser;
        public User RegUser
        {
            get
            {
                if (regUser == null)
                    regUser = new User();
                return regUser;
            }
            set { regUser = value; }
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

        }

        private void RunOpenHome(Type obj)
        {
            _eventAggregator.GetEvent<OpenHomeEvent>().Publish();
        }



        //////////////////////////////////////////// Commands:
        public ICommand OpenMyLoginCommand { get; set; }
        public ICommand OpenMyHomeCommand { get; set; }
    }
}
