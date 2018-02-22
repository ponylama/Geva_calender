using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using prjSellor.Dal;
using prjSellor.Models;
using System.Web.Security;
using System.Security.Cryptography;

namespace prjSellor.Models
{
    public class User
    {

        public const int HASH_SIZE = 24;
        public const int SALT_SIZE = 24;
        public const int PBKDF2_ITT = 500;
        public User()
        {
            firstName = "";
            lastName = "";
            userName = "";
            password = "";
            phone = "";
        }

        public User(int breakTime)
        {
            userName = "";
            firstName = "";
            lastName = "";
            phone = "";
        }

        public User(string userName)
        {
            this.userName = userName;
            DataLayer dal = new DataLayer();
            List<User> usr = (from u in dal.users
                              where (u.userName == userName)
                              select u).ToList<User>();
            if (usr.Count > 0)
            {
                this.firstName = usr[0].firstName;
                this.lastName = usr[0].lastName;
                this.phone = usr[0].phone;
            }
        }

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }


        public string type { get; set; }

        [Required(ErrorMessage = "first name is required.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "last name is required.")]
        public string lastName { get; set; }

        [Key]
        [Required(ErrorMessage = "user name is required.")]
        public string userName { get; set; }

        [Required(ErrorMessage = "password is required.")]
        [StringLength(48, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 48.")]
        public string password { get; set; }

        [Required(ErrorMessage = "phone number is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "phone number can be only numbers")]
        public string phone { get; set; }


        public bool hashPass(string purpose)
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_SIZE];
            csprng.GetBytes(salt);
            byte[] hash = PBKDF2(password, salt, PBKDF2_ITT, HASH_SIZE);
            password = Convert.ToBase64String(hash);
            return saveSalt(Convert.ToBase64String(salt), purpose);
        }

        private byte[] PBKDF2(string password, byte[] salt, int pBKDF2_ITT, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = pBKDF2_ITT;
            return pbkdf2.GetBytes(outputBytes);
        }

        private bool saveSalt(string salt, string purpose)  //purp: insert or update
        {
            SaltDal dal = new SaltDal();
            Salt saltObj = new Salt(userName, salt);
            if (purpose == "update")
            {
                dal.saltData.Attach(saltObj);
                dal.Entry(saltObj).Property(x => x.salt).IsModified = true;
            }
            else if (purpose == "signUp")
            {
                dal.saltData.Add(saltObj);
            }
            dal.SaveChanges();
            return true;
        }

        public User checkUsernamePassword()
        {
            DataLayer DataDal = new DataLayer();
            SaltDal saltDal = new SaltDal();
            {
                List<Salt> saltValidate = (from u in saltDal.saltData
                                           where (u.userName == userName)
                                           select u).ToList<Salt>();
                if (saltValidate.Count != 1)
                    return null;
                string userSalt = saltValidate[0].salt;
                List<User> usersValidate = (from u in DataDal.users
                                            where (u.userName == userName)
                                            select u).ToList<User>();
                if (usersValidate.Count != 1)
                    return null;
                password = Convert.ToBase64String(PBKDF2(password, Convert.FromBase64String(userSalt), PBKDF2_ITT, HASH_SIZE));
                if (password == usersValidate[0].password)
                {
                    FormsAuthentication.SetAuthCookie(usersValidate[0].type, true);
                    return usersValidate[0];
                }
                return null;
            }

        }




    }
}