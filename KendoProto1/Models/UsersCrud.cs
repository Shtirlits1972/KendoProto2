using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KendoProto1.Models;
using System.Web;
using System.Web.Mvc;

namespace KendoProto1.Models
{
    public class UsersCrud
    {
        public static Task<List<Users>> GetAll()
        {
            return Task.Run(() => {
                return ConvertList(DataSql.GetAll("UsersSel", null));
            });
        }
        public static Task<Users> Enter(string Email, string Pass)
        {
            SqlParameter[] param = { new SqlParameter("Email", Email),
                                     new SqlParameter("Pass", Pass) };
            return Task.Run(() =>
            {
                List<object[]> list = DataSql.GetAll("Enter", param);

                if (list != null && list.Count > 0)
                {
                    return (Users)list[0];
                }
                else
                {
                    return null;
                }

            });
        }
        public static string[] GetRoles()
        {
            string[] Roles = { "Админ", "Юзер" };
            return Roles;
        }

        public static List<Users> ConvertList(List<object[]> listOb)
        {
            List<Users> list = new List<Users>();
            for (int i = 0; i < listOb.Count; i++)
            {
                list.Add((Users)listOb[i]);
            }
            return list;
        }

        public static void Del(int Id)
        {
            Task.Run(() => {
                SqlParameter[] param = { new SqlParameter("Id", Id) };
                try
                {
                    DataSql.ScalarCommand("UsersDel", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static void Edit(Users user)
        {
            SqlParameter[] param =
                { new SqlParameter("Id", user.Id),
                new SqlParameter("Email", user.Email),
                new SqlParameter("Pass", user.Password),
                new SqlParameter("Role", user.Role),
                new SqlParameter("UserFio", user.UserFio),
                new SqlParameter("Banned", user.Banned)
            }; 
            try
            {
                DataSql.ScalarCommand("UsersUpd", param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Users GetById(int Id)
        {
            SqlParameter[] param = { new SqlParameter("Id", Id) };

            return (Users)DataSql.GetAll("UsersById", param)[0];
        }
        public static void Add(Users user)
        {
            try
            {
                SqlParameter[] param = {

                new SqlParameter("Email", user.Email),
                new SqlParameter("Pass", user.Password),
                new SqlParameter("Role", user.Role),
                new SqlParameter("UserFio", user.UserFio),
                new SqlParameter("Banned", user.Banned)};

                DataSql.ScalarCommand("UsersIns ", param);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}