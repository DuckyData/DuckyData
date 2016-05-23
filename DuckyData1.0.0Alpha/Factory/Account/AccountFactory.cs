using AutoMapper;
using DuckyData1._0._0Alpha.Models;
using DuckyData1._0._0Alpha.ViewModels.Account;
using System;
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

       public AccountFactory() {
           this.database = conn.getConnection();
       }

       public void getDatabase() {
           if (this.database == null) {
               this.database = conn.getConnection();
           }
       }

       public User_Activation_Code getRegiseInfoByCode(string activationCode) {
           getDatabase();
           User_Activation_Code userCode = database.User_Activation_Codes.First(u => u.Activation_Code == activationCode);
           if (userCode != null)
           {
               return userCode;
           }else{
               return null;
           }
       }

       public void createRegiseInfo(RegisterViewModel newUser, string code)
       {
            User_Activation_Code newRegistInfo = new User_Activation_Code(newUser.Email, newUser.Password, code);
           database.User_Activation_Codes.Add(newRegistInfo);
           database.SaveChanges();
       }

        public string createNewUser(User_Activation_Code newUser) {
            ApplicationUser userToAdd = new ApplicationUser(newUser.User_Account, newUser.Password);
            userToAdd.Id = hashEmail(newUser.User_Account);
            userDB.Users.Add(userToAdd);
            userDB.SaveChanges();
            return userToAdd.Id;
        }

        public bool updateUserInfo(userAdd userUpdate) {
            ApplicationUser user = userDB.Users.SingleOrDefault(u => u.Id == userUpdate.Id);
            if (user != null)
            {
                ApplicationUser newInfo;
                newInfo = new ApplicationUser();
                newInfo = Mapper.Map<ApplicationUser>(userUpdate);
                userDB.Entry(user).CurrentValues.SetValues(newInfo);
                userDB.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public ApplicationUser getUserById(string id) {
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

    }
} 
