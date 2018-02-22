using prjSellor.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjSellor.Models
{
    public class searchUser
    {
        public List<User> users;
        public int flag = 0;
        public searchUser()
        {
            users = new List<User>();
        }

        public searchUser(string name)
        {
            DataLayer dal = new DataLayer();
            users = (from x in dal.users where x.firstName.Contains(name) select x).ToList();
            flag = 1;
        }

    }
}