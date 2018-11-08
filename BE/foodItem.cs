using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FoodItem
    {

        public long TagId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Nutrition Nutritions { get; set; }

        public FoodItem(long id, string name, string imageUrl, Nutrition ntn = null)
        {
            TagId = id;
            Name = name;
            ImageUrl = imageUrl;
            Nutritions = ntn;
        }

    }
}