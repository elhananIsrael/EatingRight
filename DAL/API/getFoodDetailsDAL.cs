using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RapidAPISDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BE;

namespace DAL.API
{


     public class getFoodDetailsDAL
    {

        private static RapidAPI RapidApi = new RapidAPI(
                                                         "default-application_5bc602bae4b0d1763ed66bfa", 
                                                         "bd6d034f-bc34-4c63-8635-6f5c52769023"
                                                       );


        public async Task<List<FoodItem>> SearchFoodByName(string search)
        {

            List<Parameter> body = new List<Parameter> {

        new DataParameter("applicationSecret", "12707924c854726c16ec685b23b25d5e"),
        new DataParameter("foodDescription", search),
        new DataParameter("applicationId", "7dc87896")
        };

            List<FoodItem> foodSearchArr = new List<FoodItem>();
            try
            {
                var temp = await RapidApi.Call("Nutritionix", "searchFoods", body.ToArray());
                if (temp.TryGetValue("success", out object payload))
                {

                    var foodArrayCSharp = SearchFoodsJsonResults.FromJson(JsonConvert.SerializeObject(temp));
                    foreach (var item in foodArrayCSharp.Success[0].Common)
                    {
                        foodSearchArr.Add(
                            new FoodItem(
                                         item.TagId,
                                         item.FoodName,
                                         item.Photo.Thumb
                                         ));
                    }

                }
                return foodSearchArr;
            }
            catch (RapidAPIServerException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///////////////////////////////////////////


        public async Task<List<Nutrition>> GetNutritionByName(string search)
        {

            List<Parameter> body = new List<Parameter> {

        new DataParameter("applicationSecret", "12707924c854726c16ec685b23b25d5e"),
        new DataParameter("foodDescription", search),
        new DataParameter("applicationId", "7dc87896")
        };

           // nutrition Nutritions = new nutrition();
            List<Nutrition> nutritionArr = new List<Nutrition>();
            try
            {
                var temp = await RapidApi.Call("Nutritionix", "getFoodsNutrients", body.ToArray());
                if (temp.TryGetValue("success", out object payload))
                {

                    var getFoodsNutrientsJsonResults = GetFoodsNutrientsJsonResults.FromJson(JsonConvert.SerializeObject(temp));
                    foreach (var item in getFoodsNutrientsJsonResults.Success[0].Foods)
                    {
                        nutritionArr.Add(
                                          new Nutrition(
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfCalories,
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfTotalCarbohydrate,
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfTotalFat,
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfProtein,
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfSodium,
                                                         getFoodsNutrientsJsonResults.Success[0].Foods[0].NfSugars
                                                       )
                                         );
                    }

                    
                }
                return nutritionArr;
            }
            catch (RapidAPIServerException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





    }
}

