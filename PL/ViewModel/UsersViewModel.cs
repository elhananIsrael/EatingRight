using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    class UsersViewModel
    {
    }
}




/*
 דוגמא
 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMVVM.Commands;
using TestMVVM.Models;

namespace TestMVVM.ViewModels
{
    public class StudentsViewModel : INotifyPropertyChanged,ISearch
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Student> CurrentStudents { get; set; }

        private StudentsModel CurrentModel;

        public SearchCommand Search { get; set; }
        public AddCommand Add { get; set; }
        public string ColName {
            get { return CurrentModel.CollageName; }
            set { CurrentModel.CollageName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ColName"));
               }

        }

        public StudentsViewModel()
        {
            CurrentModel = new StudentsModel();//צריך קריטריונים לטעינת הנתונים
            CurrentStudents = new ObservableCollection<Student>();
            Search = new SearchCommand(this);
            Add = new AddCommand(this);
            CurrentStudents.CollectionChanged += CurrentStudents_CollectionChanged;

        }

        private void CurrentStudents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           // כאן לדאוג לעדכן את המודל אם התווסף סטודנט חדש
        }

        public void SearchStudents(string Topic)
        {
          List<Student> NewStudents= CurrentModel.GetStudentsByTopic(Topic);
            CurrentStudents.Clear();
            foreach (var Student in NewStudents)
            {
                CurrentStudents.Add(Student);
            }
            
        }

    }
}

     
     */
