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
using BE.Entitys;

namespace PL.ViewModel
{
    class ProfilVM:BaseVM
    {
        public ProfilVM()
        {
           // _eventAggregator = new Prism.Events.EventAggregator();

            myBl = new Bl();
            updateMyCurrentUser();
        }
       

        public Bl myBl;
        private IEventAggregator _eventAggregator;

        private User myCurrentUser;

        public User MyCurrentUser {
            get { return myCurrentUser; }
            set {
                if (value != null)
                    myCurrentUser = value;
                OnPropertyChanged();
            }
            
        }

        public string MyUserBirth
        {
            get { return (myCurrentUser.BirthDate.Day.ToString() + "/"+myCurrentUser.BirthDate.Month.ToString() + "/"+ myCurrentUser.BirthDate.Year.ToString() ); }
            set { ; }
        }

        public async Task updateMyCurrentUser()
        {
            if (myCurrentUser == null)
                myCurrentUser = new User();
            var tempUser = await myBl.GetCurrentUser();
            if (tempUser != null)
                MyCurrentUser = tempUser;
        }


    }
}

      