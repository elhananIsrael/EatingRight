using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using DAL.DataBase;
using System.Data.Entity;
using System.Reflection;

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
            var user = await db.Users.Where(b=> b.Email==userEmail).Include(a => a.Goals).SingleOrDefaultAsync();

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
                        goal.Date = isExistGoal.Date;                     
                        db.Entry(isExistGoal).CurrentValues.SetValues(goal);
                    }
            await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task AddMeal(Meal meal)
        {
            try
            {
                using (var db = new EatingRightDBContext())
                {

                    var userEmail = await GetCurrentUserEmail();
                    var user = await db.Users.Where(b => b.Email == userEmail).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).SingleOrDefaultAsync();

                    var isExistMeal = user.Meals.Where(A => A.Date.Year == meal.Date.Year && A.Date.Month == meal.Date.Month && A.Date.Day == meal.Date.Day).FirstOrDefault();

                    if (isExistMeal != null)
                    {
                        isExistMeal.FoodItems.Add(meal.FoodItems[0]);
                        /* isExistMeal.FoodItems.Clear();
                        foreach (var item in meal.FoodItems)
                        {
                            isExistMeal.FoodItems.Add(item);
                        }*/
                       // meal.Date = isExistMeal.Date;
                        
                      //  meal.Id = isExistMeal.Id;
                      //  meal.UserEmail = isExistMeal.UserEmail;
                       // db.Entry(isExistMeal).CurrentValues.SetValues(meal);
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

                    var isExistUserEmail = await GetCurrentUserEmail();
            if (isExistUserEmail == null)
                        db.Users.Add(user);

            await db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw new Exception();
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
                    var user = await db.Users.Where(b => b.Email == userEmail).Include(a => a.Goals).SingleOrDefaultAsync();

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
                    var user = await db.Users.Where(b => b.Email == userEmail).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).FirstOrDefaultAsync();

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
                var user = await myDb.Users
                     .Where(A => A.Email == email).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).SingleOrDefaultAsync();


                /*    (from userItem in myDb.Users
                           where userItem.Email == email
                           select userItem).SingleOrDefaultAsync();*/

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }


        public async Task<bool> IsUserInDBByEmail(string email)
        {
             var isExist= await  myDb.Users.Where(a=> a.Email==email).FirstOrDefaultAsync();
            return !isExist.Equals(null);
        }

        public async Task SetCurrentUser(string email)
        {            
            var currAcc = await myDb.CurrentAccount.SingleOrDefaultAsync();
            if (currAcc==null || currAcc.Email != email)
            {
                if (currAcc != null)
                myDb.CurrentAccount.Remove(currAcc);
                CurrentUser newUser = new CurrentUser();
                newUser.Email = email;
                myDb.CurrentAccount.Add(newUser);
              await  myDb.SaveChangesAsync();
            }
        }
    }
}
