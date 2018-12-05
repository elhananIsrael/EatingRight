using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;

namespace DAL
{
   public interface IDal
    {
        Task<string> GetCurrentUserEmail();
        Task<User> GetCurrentUser();
        Task SetCurrentUser(string email);
        Task<User> GetUserByEmail(string email);
        Task<bool> IsUserInDBByEmail(string email);
        Task AddGoal(Goal goal);
        Task<Goal> GetGoal(DateTime dateTime);
        Task AddBodyMeasurement(BodyMeasurement bodyMeasurement);
        Task<BodyMeasurement> GetBodyMeasurement(DateTime dateTime);
        Task AddMeal(Meal meal);
        Task<Meal> GetMeal(DateTime dateTime);
        Task AddUser(User user);
        Task<Nutrition> GetNutritions(string foodName);
        Task<List<FoodItem>> GetFoodItems(string name);      
        
    }
}
