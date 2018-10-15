using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
   public class Request
    {
       // protected
            public Task<Dictionary<string, object>> RequestBL(string search, string nutritionixReqType)
        {
            DAL.API.myFoodApi foodApi=new DAL.API.myFoodApi();

            return foodApi.Request(search, nutritionixReqType);
        }
    }
}
