using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BE.Entitys
{
    public class User
    {
        public User()
        {
            BirthDate = DateTime.Now;
            Meals = new List<Meal>();
            Goals = new List<Goal>();
            BodyMeasurements = new List<BodyMeasurement>();
        }
        public User(string email)
        {
            Email = email;
            BirthDate = DateTime.Now;
            Meals = new List<Meal>();
            Goals = new List<Goal>();
            BodyMeasurements = new List<BodyMeasurement>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Key]
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        
        public List<Meal> Meals { get; set; }

        public List<Goal> Goals { get; set; }

        public List<BodyMeasurement> BodyMeasurements { get; set; }

    }
}
