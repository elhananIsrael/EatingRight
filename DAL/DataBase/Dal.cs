using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Entitys;
using System.Data.Entity;


namespace DAL.DataBase
{
    class Dal
    {
        public EatingRightDBContext DbContext { get; set; }

       public User GetUserByEmail(string email)
        {
            try
            {
                var user =  (from userItem in DbContext.Users
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



    }
}
