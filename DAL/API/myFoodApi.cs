using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidAPISDK;

namespace DAL.API
{
     public class myFoodApi
        {

           // protected
           public async Task<Dictionary<string, object>> Request(string search, string nutritionixReqType)
            {

                RapidAPI RapidApi = new RapidAPI("default-application_5bc602bae4b0d1763ed66bfa", "bd6d034f-bc34-4c63-8635-6f5c52769023");


                List<Parameter> body = new List<Parameter>
            {
                new DataParameter("applicationSecret", "12707924c854726c16ec685b23b25d5e"),
                new DataParameter("foodDescription", "banana"),
                new DataParameter("applicationId", "7dc87896")
            };

                try
                {
                    return 
                    await RapidApi.Call("Nutritionix", "searchFoods", body.ToArray());
                }
                catch (RapidAPIServerException e)
                {
                    //TODOO:  need the implemet 
                }
                catch (Exception e)
                {
                    //TODOO:  need the implemet 

                }

                return null;
            
        }
    }
}


/*
using RapidAPISDK;

private static RapidAPI RapidApi = new RapidAPI("default-application_5bc602bae4b0d1763ed66bfa", "bd6d034f-bc34-4c63-8635-6f5c52769023");


List<Parameter> body = new List<Parameter>();

body.Add(new DataParameter("applicationSecret", " 12707924c854726c16ec685b23b25d5e"));
body.Add(new DataParameter("foodDescription", "banana"));
body.Add(new DataParameter("applicationId", "7dc87896"));

try { 
	Dictionary<string, object> response = RapidApi.Call("Nutritionix", "searchFoods", body.ToArray()).Result;
object payload;
	if (response.TryGetValue("success", out payload)){ 

	}
	else{ 

	} 
} 
catch (RapidAPIServerException e){ 

 }
catch (Exception e){ 

 }
 */