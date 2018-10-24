using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class foodItem
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public nutrition Nutritions { get; set; }

        public foodItem(long id, string name, string imageUrl, nutrition ntn = null)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Nutritions = null;
        }

    }
}




/*
 * 
 * 
 
    public FoodUnit()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Key]
        public String Name { get; set; }

        public string ImageUrl { get; set; }

        public Guid FoodNutritionsItemID { get; set; }




 * 
 * 
 {
            "food_name": "milk",
            "serving_unit": "cup",
            "serving_qty": 1,
            "common_type": null,
            "photo": {
              "thumb": "https://d2xdmhkmkbyw75.cloudfront.net/313_thumb.jpg"
            },
            "tag_id": "313",
            "locale": "en_US"
          },
     */
