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
            // var user = await GetCurrentUser();
            var userEmail = await GetCurrentUserEmail();
            var user = await myDb.Users.Where(b=> b.Email==userEmail).Include(a => a.Goals).SingleOrDefaultAsync();

            var isExistGoal = await GetGoal(goal.Date);
            if (isExistGoal == null)
                user.Goals.Add(goal);
            else
                myDb.Entry(isExistGoal).CurrentValues.SetValues(goal);
            await myDb.SaveChangesAsync();
        }

        public async Task AddMeal(Meal meal)
        {
            var userEmail =await GetCurrentUserEmail();
            var user = await myDb.Users.Where(b=> b.Email==userEmail).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).SingleOrDefaultAsync();

            var isExistMeal =await GetMeal(meal.Date);
            if (isExistMeal != null)
                myDb.Entry(isExistMeal).CurrentValues.SetValues(meal);
            else
                user.Meals.Add(meal);

            await myDb.SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            var isExistUserEmail = await GetCurrentUserEmail();
            if (isExistUserEmail == null)
                myDb.Users.Add(user);

            await myDb.SaveChangesAsync();

        }

        /*public async Task<User> GetCurrentUser()
        {

            var currentUserEmail = await GetCurrentUserEmail();
                if (currentUserEmail == null)
                {
                    return null;
                }
                return await GetUserByEmail(currentUserEmail);
           
        }*/

        public async Task<string> GetCurrentUserEmail()
        {
            var currentUser = await myDb.CurrentAccount.FirstOrDefaultAsync();
            if (currentUser == null)
            {
                return null;
            }
            return currentUser.Email;
        }

        public async Task<List<FoodItem>> GetFoodItems(string name)
        {
            return await FoodAPI.SearchFoodByName(name);
        }

        public async Task<Goal> GetGoal(DateTime dateTime)
        {
            try
            {
                var userEmail = await GetCurrentUserEmail();
                var user = await myDb.Users.Where(b => b.Email == userEmail).Include(a => a.Goals).SingleOrDefaultAsync();

               // var user =await GetCurrentUser();
                var myGoal =  user.Goals
                .Where(A => A.Date == dateTime ).FirstOrDefault() ;
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
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<Meal> GetMeal(DateTime dateTime)
        {
            try
            {
                // var user =await GetCurrentUser();
                var userEmail = await GetCurrentUserEmail();
                var user = await myDb.Users.Where(b => b.Email == userEmail).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).FirstOrDefaultAsync();

                if (user != null)
                {
                    var myMeal = user.Meals
                .Where(A => A.Date.Year == dateTime.Year && A.Date.Month == dateTime.Month &&  A.Date.Day == dateTime.Day).FirstOrDefault();

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
            catch (Exception)
            {

                throw new Exception();
            }
        }

        public async Task<Nutrition> GetNutritions(string foodName)
        {
            return await FoodAPI.GetNutritionsByName(foodName);
        }

    
   //     public async Task<User> GetUserByEmail(string email)
   //     {
   //    try
   //    {
   //        var user = await myDb.Users
   //             .Where(A => A.Email == email).Include(a => a.Meals).Include(a => a.Meals.Select(m => m.FoodItems)).Include(a => a.Goals).SingleOrDefaultAsync();


   //         /*    (from userItem in myDb.Users
   //                    where userItem.Email == email
   //                    select userItem).SingleOrDefaultAsync();*/

   //        if (user == null)
   //        {
   //            return null;
   //        }
   //        return user;
   //    }
   //    catch (Exception)
   //    {

   //        throw new Exception();
   //    }
   //}


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
