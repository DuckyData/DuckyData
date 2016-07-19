using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DuckyData1._0._0Alpha.Factory.Account
{
    public class AccountFactory
    {
        private DatabaseConnection conn = new DatabaseConnection();
        private DataContext database;
        private ApplicationDbContext userDB = new ApplicationDbContext();

        public AccountFactory()
        {
            this.database = conn.getConnection();
        }

        public void getDatabase()
        {
            if (this.database == null)
            {
                this.database = conn.getConnection();
            }
        }

        public IEnumerable<userAdd> getUserList(string searchString)
        {

            var userList = from u in userDB.Users select u;
            if (!String.IsNullOrEmpty(searchString))
            {
                userList = userList.Where(s => s.lastName.Contains(searchString)
                                      || s.firstName.Contains(searchString));
            }
            userList = userList.OrderBy(us => us.firstName);
            getDatabase();

            return Mapper.Map<IEnumerable<userAdd>>(userList);
        }

        public User_Activation_Code getRegiseInfoByCode(string activationCode)
        {
            getDatabase();
            User_Activation_Code userCode = database.User_Activation_Codes.First(u => u.Activation_Code == activationCode);
            if (userCode != null)
            {
                return userCode;
            }
            else
            {
                return null;
            }
        }

        public void createRegiseInfo(RegisterViewModel newUser, string code)
        {
            User_Activation_Code newRegistInfo = new User_Activation_Code(newUser.Email, newUser.Password, code);
            userDB.User_Activation_Codes.Add(newRegistInfo);
            userDB.SaveChanges();
        }

        public string createNewUser(User_Activation_Code newUser)
        {
            ApplicationUser userToAdd = new ApplicationUser(newUser.User_Account, newUser.Password);
            userToAdd.Id = hashEmail(newUser.User_Account);
            userDB.Users.Add(userToAdd);
            userDB.SaveChanges();
            return userToAdd.Id;
        }

        public ApplicationUser findUserByEmail(string email)
        {
            ApplicationUser user = userDB.Users.First(u => u.Email == email);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public ApplicationUser findUserById(string id)
        {
            ApplicationUser user = userDB.Users.First(u => u.Id == id);

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }

        }

        public bool updateUserInfo(userAdd userUpdate)
        {
            ApplicationUser user = userDB.Users.SingleOrDefault(u => u.Id == userUpdate.Id);

            user.firstName = userUpdate.FirstName;
            user.lastName = userUpdate.LastName;
            user.UserName = userUpdate.Email;
            userDB.SaveChanges();
            return true;
        }

        public void adminUpdateUserInfo(ApplicationUser dest, adminEditUser src)
        {

            dest.firstName = src.FirstName;
            dest.lastName = src.LastName;
            dest.PhoneNumber = src.PhoneNumber;
            userDB.SaveChanges();
        }

        public ApplicationUser getUserById(string id)
        {
            ApplicationUser user = userDB.Users.First(u => u.Id == id);
            return user;
        }

        public string hashEmail(string email)
        {
            SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();
            provider.ComputeHash(ASCIIEncoding.ASCII.GetBytes(email));
            byte[] re = provider.Hash;
            StringBuilder hashValue = new StringBuilder();
            foreach (byte b in re)
            {
                hashValue.Append(b.ToString("x2"));
            }
            return hashValue.ToString();
        }

        public bool resetCode(string email, string code)
        {
            getDatabase();
            User_Activation_Code userCode = userDB.User_Activation_Codes.FirstOrDefault(u => u.User_Account == email);
            if (userCode != null)
            {
                userCode.Activation_Code = code;
                userDB.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User_Activation_Code findUserCodeByCode(string code)
        {
            getDatabase();
            User_Activation_Code userCode = userDB.User_Activation_Codes.FirstOrDefault(u => u.Activation_Code == code);

            if (userCode != null)
            {
                return userCode;
            }
            else
            {
                return null;
            }
        }

    }
}
