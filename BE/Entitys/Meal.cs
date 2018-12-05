using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BE.Entitys
{
    public class Meal
    {
        public Meal()
        {
            FoodItems = new List<FoodItem>();
            Date = DateTime.Now;
            Id = Guid.NewGuid();
           // UserEmail = "";
        }

         [Key]
         public Guid Id { get; set; }

     
        public DateTime Date { get; set; }
        
     //   public string UserEmail { get; set; }
       [Required]
        public List<FoodItem> FoodItems { get; set; }

    }
}
