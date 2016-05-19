using DuckyData1._0._0Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DuckyData1._0._0Alpha.Factory.Account
{
    public class AccountFactory
    {
        private DatabaseConnection conn = new DatabaseConnection();
        private DataContext database;

        public AccountFactory() {
            this.database = conn.getConnection();
        }

        public User_Activation_Code getActivateCodeByAccount(RegisterViewModel userRegister) {

            User_Activation_Code userCode = database.User_Activation_Codes.First(u=>u.User_Account == userRegister.Email);

            if (userCode != null)
            {
                return userCode;
            }else{
                return null;
            }
        }
    }
}