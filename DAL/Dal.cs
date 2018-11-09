using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using DAL.DataBase;
using System.Data.Entity;


namespace DAL
{
    class Dal:IDal
    {
        public Dal()
        {
            FoodAPI = new API.getFoodDetailsDAL();
        }

        private EatingRightDBContext DbContext { get; set; }
        private readonly API.getFoodDetailsDAL FoodAPI;



        public Task AddGoal(Goal goal)
        {
          //  var currentUserEmail=this.GetCurrentUserEmail();
          //DbContext.Users


            throw new NotImplementedException();
        }

        public Task AddMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Task AddUser(User goal)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCurrentUserEmail()
        {
            throw new NotImplementedException();
        }

        public Task<List<FoodItem>> GetFoodItems(string name)
        {
            return FoodAPI.SearchFoodByName(name);
        }

        public Task<Goal> GetGoal(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<Meal> GetMeal(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Task<Nutrition> GetNutritions(string foodName)
        {
            return FoodAPI.GetNutritionsByName(foodName);
        }

        public Task GetUser(User goal)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
       try
       {
           var user = (from userItem in DbContext.Users
                       where userItem.Email == email
                       select userItem).SingleOrDefault();

           if (user == null)
           {
               throw new Exception();
           }
           return user;
       }
       catch (Exception)
       {

           throw new Exception();
       }
   }


        public bool IsUserInDBByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void SetCurrentUser(string email)
        {
            throw new NotImplementedException();
        }
    }
}
