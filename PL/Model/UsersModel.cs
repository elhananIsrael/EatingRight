using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Model
{
    class UsersModel
    {
    }
}



/*
 דוגמא:
  ////////////////////////בתוך הBE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM
{
   public class Student
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public string Name { get; set; }

        public float Average { get; set; }
    }
}



    ///////////////////////////////



    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMVVM.Models
{
   public class StudentsModel
    {

        public string CollageName { get; set; }
        public List<Student> CurrentStudents { get; set; }

        public StudentsModel()
        {
            CollageName = "מבחר";
        }

        public List<Student> GetStudentsByTopic(string Topic)
        {
            // mocking data retrival

            var students = new List<Student>
            {
                new Student {Id=111,Topic="biology",Name="dani choen",Average=89.4f},
                new Student {Id=222,Topic="biology",Name="dani choen",Average=79.4f},
                new Student {Id=112,Topic="physics",Name="kobi mimoni",Average=95.4f},
                new Student {Id=121,Topic="chemistry",Name="roni daromi",Average=90.4f},
                new Student {Id=411,Topic="biology",Name="dani zanani",Average=78.4f},
                new Student {Id=611,Topic="chemistry",Name="dani levi",Average=66.4f},
                new Student {Id=711,Topic="physics",Name="roni choen",Average=92.4f},
            };

            var Selected = (from s in students
                            where s.Topic.Contains(Topic)
                            select s).ToList<Student>();

            return Selected;
        }

      
    }
}




 */
