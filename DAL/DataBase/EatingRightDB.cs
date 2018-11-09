using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BE.Entitys;

namespace DAL.DataBase
{

    public class EatingRightDBContext : DbContext
    {
        public EatingRightDBContext() : base("EatingRightDB")
        {
            if (!Database.Exists())
            {
                Database.Create();
                //LoadJson();
            }

        }

        public DbSet<User> Users { get; set; }

        public DbSet<CurrentUser> CurrentAccount { get; set; }


    }


}

    