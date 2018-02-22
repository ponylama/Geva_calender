using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace prjSellor.Models
{
    public class Salt
    {
        public Salt()
        {
            userName = "";
            salt = "";
        }
        public Salt(string userName, string salt)
        {
            this.userName = userName;
            this.salt = salt;
        }

        [Key]
        public string userName { get; set; }

        public string salt { get; set; }
    }
}