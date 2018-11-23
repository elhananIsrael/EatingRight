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
        }

        [Key]
        public DateTime Date { get; set; }

       
        public List<FoodItem> FoodItems { get; set; }

    }
}
