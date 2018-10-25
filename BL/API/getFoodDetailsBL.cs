using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL.API
{
    public class getFoodDetailsBL
    {
        DAL.API.getFoodDetailsDAL gfdDAL = new DAL.API.getFoodDetailsDAL();


        public Task<List<foodItem>> SearchFoodByName(string search)
        {
            return gfdDAL.SearchFoodByName(search);
        }

        public Task<nutrition> GetNutritionByName(string search)
        {
            return gfdDAL.GetNutritionByName(search);
        }
    }
}
