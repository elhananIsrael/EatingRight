using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE.Entitys;

namespace BL
{
    interface IBl: IDal
    {
        Task<Goal> GetPercentageOfTheMealFromGoal(DateTime dateTime);

    }
}
