using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using BE.function;


namespace BL.API
{
    public class getFoodDetailsBL
    {
        DAL.API.getFoodDetailsDAL gfdDAL = new DAL.API.getFoodDetailsDAL();


        public Task<List<FoodItem>> SearchFoodByName(string search)
        {
            return gfdDAL.SearchFoodByName(search);
        }

        public Task<Nutrition> GetNutritionByName(string search)
        {
            return gfdDAL.GetNutritionsByName(search);            
        }
    }
}
