using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE.Entitys
{
    public class FoodItem
    {
        
            public FoodItem()
        {
             Id = Guid.NewGuid();
            Nutritions = new Nutrition();
        }
            
             [Key]
        public Guid Id { get; set; }

        public long TagId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Nutrition Nutritions { get; set; }

        public FoodItem(long id, string name, string imageUrl, Nutrition ntn = null)
        {
            TagId = id;
            this.Name = name;
            this.ImageUrl = imageUrl;
            Nutritions = ntn;
        }

    }
}