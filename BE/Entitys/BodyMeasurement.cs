using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE.Entitys
{
   public class BodyMeasurement
    {

        public BodyMeasurement()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Weight = 0;
            Height = 0;
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }        


    }
}
