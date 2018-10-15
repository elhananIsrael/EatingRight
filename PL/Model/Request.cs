using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace PL.Model
{
    
        class Request
        {
            //protected
            public Task<Dictionary<string, object>> RequestPL(string search, string nutritionixReqType)
            {
                BL.Request foodApi = new BL.Request();

                return foodApi.RequestBL(search, nutritionixReqType);
            }
        }
    
}
