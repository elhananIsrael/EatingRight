using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;

namespace DAL
{
    interface IDal
    {
        Task<string> GetCurrentUserEmail();
        Task<User> GetCurrentUser();
        void SetCurrentUser(string email);
        User GetUserByEmail(string email);
        bool IsUserInDBByEmail(string email);
        Task AddGoal(Goal goal);
        Task<Goal> GetGoal(DateTime dateTime);
        Task AddMeal(Meal meal);
        Task<Meal> GetMeal(DateTime dateTime);
        Task AddUser(User goal);
        Task GetUser(User goal);
        Task<Nutrition> GetNutritions(string foodName);
        Task<List<FoodItem>> GetFoodItems(string name);      
        
    }
}
