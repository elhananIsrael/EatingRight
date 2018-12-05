using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using DAL;

namespace BL
{
   public class Bl : IBl
    {
        Dal myDal;
        public Bl()
        {
            myDal = new Dal();
        }

        public async Task AddBodyMeasurement(BodyMeasurement bodyMeasurement)
        {
            await myDal.AddBodyMeasurement(bodyMeasurement);
        }

        public async Task AddGoal(Goal goal)
        {
           await myDal.AddGoal(goal);
        }

        public async Task AddMeal(Meal meal)
        {
           await myDal.AddMeal(meal);
        }

        public async Task AddUser(User user)
        {
          await  myDal.AddUser(user);
        }

        public async Task<BodyMeasurement> GetBodyMeasurement(DateTime dateTime)
        {
            return await myDal.GetBodyMeasurement(dateTime);
        }

        public async Task<User> GetCurrentUser()
        {
            return await myDal.GetCurrentUser();
        }

        public async Task<string> GetCurrentUserEmail()
        {
           return await myDal.GetCurrentUserEmail();
        }

        public async Task<List<FoodItem>> GetFoodItems(string name)
        {
            return await myDal.GetFoodItems(name);
        }

        public async Task<Goal> GetGoal(DateTime dateTime)
        {
            return await myDal.GetGoal(dateTime);
        }

        public async Task<Meal> GetMeal(DateTime dateTime)
        {
            return await myDal.GetMeal(dateTime);
        }

        public async Task<Nutrition> GetNutritions(string foodName)
        {
            return await myDal.GetNutritions(foodName);
        }

        public async Task<Goal> GetPercentageOfTheMealFromGoal(DateTime dateTime)
        {
            try
            {
                Goal myGoal = new Goal();
                Meal myMael = new Meal();
                Goal PercentageGoal = new Goal();
                myGoal = await myDal.GetGoal(dateTime);
                if (myGoal == null)
                    myGoal = new Goal();
                myMael = await myDal.GetMeal(dateTime);
                if (myMael == null)
                    myMael = new Meal();
                double? allCaloriesToday = 0;
                double? allCarbohydrateToday = 0;
                double? allFatToday = 0;
                double? allProteinToday = 0;
                double? allSodiumToday = 0;
                double? allSugarToday = 0;

                foreach (var foodItem in myMael.FoodItems)
                {
                    allCaloriesToday += foodItem.Nutritions.Calories;
                    allCarbohydrateToday += foodItem.Nutritions.Carbohydrate;
                    allFatToday += foodItem.Nutritions.Fat;
                    allProteinToday += foodItem.Nutritions.Protein;
                    allSodiumToday += foodItem.Nutritions.Sodium;
                    allSugarToday += foodItem.Nutritions.Sugar;
                }
                PercentageGoal.Calories =allCaloriesToday / myGoal.Calories;
                PercentageGoal.Carbohydrate = allCarbohydrateToday / myGoal.Carbohydrate;
                PercentageGoal.Fat = allFatToday / myGoal.Fat;
                PercentageGoal.Protein =allProteinToday / myGoal.Protein;
                PercentageGoal.Sodium = allSodiumToday / myGoal.Sodium;
                PercentageGoal.Sugar = allSugarToday / myGoal.Sugar;

                  if (PercentageGoal.Calories.Equals(double.NaN) || PercentageGoal.Calories == 0 || allCaloriesToday==0 || allCaloriesToday == null)
                        PercentageGoal.Calories = 0;
                  else if(myGoal.Calories==0 || myGoal.Calories==null)
                    PercentageGoal.Calories = 1;
                else PercentageGoal.Calories=  double.Parse(PercentageGoal.Calories.ToString().Substring(0, 4));

                if (PercentageGoal.Carbohydrate.Equals(double.NaN) || PercentageGoal.Carbohydrate==0 || allCarbohydrateToday == 0 || allCarbohydrateToday == null)
                    PercentageGoal.Carbohydrate = 0;
                else if (myGoal.Carbohydrate == 0 || myGoal.Carbohydrate == null)
                    PercentageGoal.Carbohydrate = 1;
                else PercentageGoal.Carbohydrate = double.Parse(PercentageGoal.Carbohydrate.ToString().Substring(0, 4));

                if (PercentageGoal.Fat.Equals(double.NaN) || PercentageGoal.Fat == 0 || allFatToday == 0 || allFatToday == null)
                    PercentageGoal.Fat = 0;
                else if (myGoal.Fat == 0 || myGoal.Fat == null)
                    PercentageGoal.Fat = 1;
                else PercentageGoal.Fat = double.Parse(PercentageGoal.Fat.ToString().Substring(0, 4));

                if (PercentageGoal.Protein.Equals(double.NaN) || PercentageGoal.Protein == 0 || allProteinToday == 0 || allProteinToday == null)
                    PercentageGoal.Protein = 0;
                else if (myGoal.Protein == 0 || myGoal.Protein == null)
                    PercentageGoal.Protein = 1;
                else PercentageGoal.Protein = double.Parse(PercentageGoal.Protein.ToString().Substring(0, 4));

                if (PercentageGoal.Sodium.Equals(double.NaN) || PercentageGoal.Sodium == 0 || allSodiumToday == 0 || allSodiumToday == null)
                    PercentageGoal.Sodium = 0;
                else if (myGoal.Sodium == 0 || myGoal.Sodium == null)
                    PercentageGoal.Sodium = 1;
                else PercentageGoal.Sodium = double.Parse(PercentageGoal.Sodium.ToString().Substring(0, 4));

                if (PercentageGoal.Sugar.Equals(double.NaN) || PercentageGoal.Sugar == 0 || allSugarToday == 0 || allSugarToday == null)
                    PercentageGoal.Sugar = 0;
                else if (myGoal.Sugar == 0 || myGoal.Sugar == null)
                    PercentageGoal.Sugar = 1;
                else PercentageGoal.Sugar = double.Parse(PercentageGoal.Sugar.ToString().Substring(0, 4));       


                return PercentageGoal;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await myDal.GetUserByEmail(email);
        }

        public async Task<bool> IsUserInDBByEmail(string email)
        {
            return await myDal.IsUserInDBByEmail(email);
        }

        public bool IsValidMailAddress(string mail)
        {

                try
                {
                    System.Net.Mail.MailAddress mailAddress = new System.Net.Mail.MailAddress(mail);

                    return true;
                }
                catch
                {
                    return false;
                }
          
        }

        public async Task SetCurrentUser(string email)
        {
            await myDal.SetCurrentUser(email);
        }
    }
}
