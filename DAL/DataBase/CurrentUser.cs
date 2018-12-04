using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DAL.DataBase
{
    public class CurrentUser
    {

        public CurrentUser()
        {
            Id = Guid.NewGuid();
            Email = "";
        }
        [Key]
        public Guid Id { get; set; }

     
        public string Email { get; set; }
    }
}
