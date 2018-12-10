using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using DAL.DataBase;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Data.Entity.Infrastructure;

namespace DAL
{
    public class Dal:IDal
    {
        public Dal()
        {
            FoodAPI = new API.getFoodDetailsDAL();
            myDb = new EatingRightDBContext();
        }

        private EatingRightDBContext myDb { get; set; }
        private readonly API.getFoodDetailsDAL FoodAPI;



        public async Task AddGoal(Goal goal)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {
                    // var user = await GetCurrentUser();
                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users
                                           .Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();

                    var isExistGoal = user.Goals.Where(A => A.Date.Year == goal.Date.Year && A.Date.Month == goal.Date.Month && A.Date.Day == goal.Date.Day).FirstOrDefault();

                    foreach (PropertyInfo p in goal.GetType().GetProperties())
                    {
                        if (p.GetType() != goal.Date.GetType() && p.GetValue(goal) == null)
                            p.SetValue(goal, 0.00, null);
                    }

                    if (isExistGoal == null)
                        user.Goals.Add(goal);                   
                    else
                    {

                      // goal.Date = isExistGoal.Date;                     
                      // db.Entry(isExistGoal).CurrentValues.SetValues(goal);

                         isExistGoal.Calories = goal.Calories;
                         isExistGoal.Carbohydrate = goal.Carbohydrate;
                         isExistGoal.Fat = goal.Fat;
                         isExistGoal.Protein = goal.Protein;
                         isExistGoal.Sodium = goal.Sodium;
                         isExistGoal.Sugar = goal.Sugar;
                    }
                 // db.Users.AddOrUpdate(user);
                         await db.SaveChangesAsync();
                   
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task AddMeal(Meal meal)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {

                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users.Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();

                    var isExistMeal = user.Meals.Where(A => A.Date.Year == meal.Date.Year && A.Date.Month == meal.Date.Month && A.Date.Day == meal.Date.Day).FirstOrDefault();

                    if (isExistMeal != null)
                    {
                        isExistMeal.FoodItems.Add(meal.FoodItems[0]);
                       
                    }
                    else
                    {
                        user.Meals.Add(meal);
                    }

                    await db.SaveChangesAsync();                      

                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task AddUser(User user)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {

                    var isExistUserEmail = await IsUserInDBByEmail(user.Email);
                    if (!isExistUserEmail)
                    {
                        db.Users.Add(user);

                        await db.SaveChangesAsync();
                    }
                    else throw new Exception("User Email Already Exist!");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<User> GetCurrentUser()
        {

            var currentUserEmail = await GetCurrentUserEmail();
                if (currentUserEmail == null)
                {
                    return null;
                }
                return await GetUserByEmail(currentUserEmail);
           
        }

        public async Task<string> GetCurrentUserEmail()
        {
            using (var db = new EatingRightDBContext())
            {
                var currentUser = await db.CurrentAccount.FirstOrDefaultAsync();
                if (currentUser == null)
                {
                    return null;
                }
                return currentUser.Email;
            }
        }

        public async Task<List<FoodItem>> GetFoodItems(string name)
        {
            return await FoodAPI.SearchFoodByName(name);
        }

        public async Task<Goal> GetGoal(DateTime dateTime)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {

                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users.Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();


                    // var user =await GetCurrentUser();
                    var myGoal = user.Goals
                    .Where(A => A.Date.Year == dateTime.Year && A.Date.Month == dateTime.Month && A.Date.Day == dateTime.Day).FirstOrDefault(); ;
                    /*  (from userItem in myDb.Users
                                    where userItem.Email == user.Email
                                    select (from goalItem in userItem.Goals
                                            where goalItem.Date == dateTime
                                            select goalItem)).SingleOrDefault();*/

                    if (myGoal == null)
                    {
                        return null;
                    }
                    return myGoal;
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<Meal> GetMeal(DateTime dateTime)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {
                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users.Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();

                    if (user != null)
                    {
                        var myMeal = user.Meals
                    .Where(A => A.Date.Year == dateTime.Year && A.Date.Month == dateTime.Month && A.Date.Day == dateTime.Day).FirstOrDefault();

                        /*(from mealItem in user.Meals
                                  where mealItem.Date == dateTime
                                  select mealItem).SingleOrDefault();*/

                        if (myMeal == null)
                        {
                            return null;
                        }
                        return myMeal;
                    }
                    else return null;
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<Nutrition> GetNutritions(string foodName)
        {
            return await FoodAPI.GetNutritionsByName(foodName);
        }


        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {
                    var user = await db.Users
                                          .Where(A => A.Email.Equals(email)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();


                    if (user == null)
                    {
                        return null;
                    }
                    return user;
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


        public async Task<bool> IsUserInDBByEmail(string email)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {
                    var isExist= await  db.Users.Where(a=> a.Email==email).FirstOrDefaultAsync();
            if (isExist == null)
                return false;
            else return true;
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task SetCurrentUser(string email)
        {

            try
            {
                using (var db = new EatingRightDBContext())
                {
                    var currAcc = await db.CurrentAccount.SingleOrDefaultAsync();
                    var user = await db.Users.Where(A => A.Email.Equals(email)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();


                    if (user == null)
                            throw new Exception("Invalid user!");

                    if (currAcc == null)
                    {                   
                        currAcc = new CurrentUser();
                    }
                        currAcc.Email = user.Email;
                        db.CurrentAccount.AddOrUpdate(currAcc);                    
               
                    await db.SaveChangesAsync();

                  //  }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task AddBodyMeasurement(BodyMeasurement bodyMeasurement)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {
                    // var user = await GetCurrentUser();
                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users
                                         .Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();

                    var isExistBodyMeasurement = user.BodyMeasurements.Where(A => A.Date.Year == bodyMeasurement.Date.Year && A.Date.Month == bodyMeasurement.Date.Month && A.Date.Day == bodyMeasurement.Date.Day).FirstOrDefault();

                    foreach (PropertyInfo p in bodyMeasurement.GetType().GetProperties())
                    {
                        if (p.GetType() != bodyMeasurement.Date.GetType() && p.GetValue(bodyMeasurement) == null)
                            p.SetValue(bodyMeasurement, 0.00, null);
                    }

                    if (isExistBodyMeasurement == null)
                        user.BodyMeasurements.Add(bodyMeasurement);
                    else
                    {

                        // goal.Date = isExistGoal.Date;                     
                        // db.Entry(isExistGoal).CurrentValues.SetValues(goal);

                        isExistBodyMeasurement.Weight = bodyMeasurement.Weight;
                        isExistBodyMeasurement.Height = bodyMeasurement.Height;

                    }
                    // db.Users.AddOrUpdate(user);
                    await db.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<BodyMeasurement> GetBodyMeasurement(DateTime dateTime)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {

                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users.Where(A => A.Email.Equals(userEmail)).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).Include(a => a.BodyMeasurements).SingleOrDefaultAsync();


                    // var user =await GetCurrentUser();
                    var myBodyMeasurement = user.BodyMeasurements.Where(A => A.Date.Year == dateTime.Year && A.Date.Month == dateTime.Month && A.Date.Day == dateTime.Day).FirstOrDefault();


                    if (myBodyMeasurement == null)
                    {
                        return null;
                    }
                    return myBodyMeasurement;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
