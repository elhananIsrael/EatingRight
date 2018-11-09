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
            Date = DateTime.Now;
        }

        [Key]
        public DateTime Date { get; set; }

        public double? Calories { get; set; }

        public double? Carbohydrate { get; set; }

        public double? Fat { get; set; }

        public double? Protein { get; set; }

        public double? Sodium { get; set; }

        public double? Sugar { get; set; }
    }
}
