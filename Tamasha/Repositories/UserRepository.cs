using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tamasha.Database;
using Tamasha.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tamasha.Repositories
{
    internal class UserRepository
    {
        
        public void Register(string email, string username, string password, DateTime date)
        {
            string[] parameters = ["@Email","@Username","@Password","@RegistryDate"];
            object[] values = [email,username,password,date];
            string storeProcedureName = "RegisterUser";
            SQL.ExecuteNonQueryStoreProcedure(storeProcedureName, parameters, values);
        }
        
        
        public List<string> Login(string email, string password)
        {
            string[] parameters = ["@Email", "@Password"];
            object[] values = [email,password];
            var result = SQL.ExecuteReaderStoreProcedure("LoginUser",parameters,values);
            return result;
        }
        
        public List<User> GetAllUsers()
        {
            var result = SQL.ExecuteReaderStoreProcedure("GetAllUsers");
            List<User> users = new List<User>();
            for (int i = 0;result.Count > i;i++)
            {
                users.Add(stringToUser(result[i].Split(";")));
            }
            if (users.Count > 0)
            {
                return users;
            }
            else return null;
        }

        public bool FindUserByUsername(string username)
        {
            var result = SQL.ExecuteReaderStoreProcedure("FindUserByUsername", ["@Username"], [username]);
            if (result.Count> 0) 
            {
                return true;
            }
            return false;
        }

        public int GetUserIdByUsername(string username)
        {
            var result = SQL.ExecuteReaderStoreProcedure("GetUserIDByUsername", ["@Username"], [username]);
            var userId = result[0].Replace(";", "");
            return Convert.ToInt32(userId);
        }

        public void UpdateUsername(string newUsername, string oldUsername)
        {
            SQL.ExecuteNonQueryStoreProcedure("UpdateUsername", ["@NewUsername", "@OldUsername"], [newUsername, oldUsername]);
        }

        public void DeleteUserByUsername(string username)
        {
            SQL.ExecuteNonQueryStoreProcedure("DeleteUserByUsername", ["@Username"], [username]);
        }

        public User stringToUser(string[] strings)
        {
            User user = new User();
            user.Email = strings[0].ToString();
            user.Username = strings[1].ToString();
            user.Password = strings[2].ToString();
            user.RegistryDate = DateTime.Parse(strings[3].ToString());
            return user;
        }
    }
}
