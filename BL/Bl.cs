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

        //public async Task<User> GetCurrentUser()
        //{
        //   return await myDal.GetCurrentUser();
        //}

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

        //public async Task<User> GetUserByEmail(string email)
        //{
        //    return await myDal.GetUserByEmail(email);
        //}

        public async Task<bool> IsUserInDBByEmail(string email)
        {
            return await myDal.IsUserInDBByEmail(email);
        }

        public async Task SetCurrentUser(string email)
        {
            await myDal.SetCurrentUser(email);
        }
    }
}
