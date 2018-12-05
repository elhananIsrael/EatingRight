using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE.Entitys
{
    public class Goal
    {
        public Goal()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
          //  UserEmail = "";
            Calories = 0;
            Carbohydrate = 0;
            Fat = 0;
            Protein = 0;
            Sodium = 0;
            Sugar = 0;
        }

         [Key]
         public Guid Id { get; set; }
       
        public DateTime Date { get; set; }
        
      //  public string UserEmail { get; set; }

        public double? Calories { get; set; }

        public double? Carbohydrate { get; set; }

        public double? Fat { get; set; }

        public double? Protein { get; set; }

        public double? Sodium { get; set; }

        public double? Sugar { get; set; }
    }
}
