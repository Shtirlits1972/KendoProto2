using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using KendoProto1.Models;
using System.ComponentModel.DataAnnotations;

namespace KendoProto1.Models
{
    public static class StaffCrud
    {

        public static Task<List<Staff>> GetAllAsync(string StaffName = "", DateTime? BirthdayStart = null, DateTime? BirthdayFinish = null, int QtyMin = -1000000000, int QtyMax = 1000000000, decimal SquareMin = -1000000000, decimal SquareMax = 1000000000, bool? IsAdmin = null, string Country1 = "", string Country2 = "", string Sex = "")
        {

            if (BirthdayStart == null)
            {
                BirthdayStart = DateTime.MinValue;
            }

            if (BirthdayFinish == null)
            {
                BirthdayFinish = DateTime.MaxValue;
            }

            return Task.Run(() =>
            {
                SqlParameter[] param = {
                    new SqlParameter("StaffName", StaffName),
                    new SqlParameter("BirthdayStart", BirthdayStart), new SqlParameter("BirthdayFinish", BirthdayFinish),
                    new SqlParameter("QtyMin", QtyMin), new SqlParameter("QtyMax", QtyMax),
                    new SqlParameter("SquareMin", SquareMin), new SqlParameter("SquareMax", SquareMax),
                    new SqlParameter("IsAdmin", IsAdmin),
                    new SqlParameter("Country1", Country1),
                    new SqlParameter("Country2", Country2),
                    new SqlParameter("Sex", Sex) };

                return ConvertList(DataSql.GetAll("StaffSel", param));
            });
        }

        public static List<Staff> GetAll(string StaffName = "", DateTime? BirthdayStart = null, DateTime? BirthdayFinish = null, int QtyMin = -1000000000, int QtyMax = 1000000000, decimal SquareMin = -1000000000, decimal SquareMax = 1000000000, bool? IsAdmin = null, string Country1 = "", string Country2 = "", string Sex = "")
        {
            if (BirthdayStart == null)
            {
                BirthdayStart = new DateTime(1900,1,1);
            }

            if (BirthdayFinish == null)
            {
                BirthdayFinish = new DateTime(2100, 1, 1);
            }

            SqlParameter[] param = {
                    new SqlParameter("StaffName", StaffName),
                    new SqlParameter("BirthdayStart", BirthdayStart), new SqlParameter("BirthdayFinish", BirthdayFinish),
                    new SqlParameter("QtyMin", QtyMin), new SqlParameter("QtyMax", QtyMax),
                    new SqlParameter("SquareMin", SquareMin), new SqlParameter("SquareMax", SquareMax),
                    new SqlParameter("IsAdmin", IsAdmin),
                    new SqlParameter("Country1", Country1),
                    new SqlParameter("Country2", Country2),
                    new SqlParameter("Sex", Sex) };

            return ConvertList(DataSql.GetAll("StaffSel", param));
        }

        public static Task<Staff> GetById(int Id)
        {
            return Task.Run(() =>
            {
                SqlParameter[] param = { new SqlParameter("Id", Id) };

                return (Staff)DataSql.GetAll("StaffById", param)[0];
            });
        }

        public static async Task Edit(Staff Staff)
        {
            await Task.Run(() => {
                SqlParameter[] param = { new SqlParameter("Id", Staff.Id),
                new SqlParameter("StaffName", Staff.StaffName),
                new SqlParameter("Birthday", Staff.Birthday),
                new SqlParameter("Qty", Staff.Qty),
                new SqlParameter("Square", Staff.Square),
                new SqlParameter("IsAdmin", Staff.IsAdmin),
                new SqlParameter("Country1", Staff.Country1.Id),
                new SqlParameter("Country2", Staff.Country2.Id),
                new SqlParameter("Sex", Staff.Sex),
                new SqlParameter("Deskr", Staff.Deskr),
                new SqlParameter("Foto", Staff.Foto) };
                try
                {
                    DataSql.ScalarCommand("StaffUpd", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static Task<int> Add(Staff Staff)
        {
            return Task.Run(() => {
                SqlParameter[] param = {
                new SqlParameter("StaffName", Staff.StaffName),
                new SqlParameter("Birthday", Staff.Birthday),
                new SqlParameter("Qty", Staff.Qty),
                new SqlParameter("Square", Staff.Square),
                new SqlParameter("IsAdmin", Staff.IsAdmin),
                new SqlParameter("Country1", Staff.Country1.Id),
                new SqlParameter("Country2", Staff.Country2.Id),
                new SqlParameter("Sex", Staff.Sex),
                new SqlParameter("Deskr", Staff.Deskr),
                new SqlParameter("Foto", Staff.Foto) };
                int res = 0;
                try
                {
                    object ob = DataSql.ScalarCommand("StaffIns ", param);
                    int.TryParse(ob.ToString(), out res);
                }
                catch (Exception ex)
                {
                    res = 0;
                }
                return res;
            });
        }

        public static async Task Del(int Id)
        {
            await Task.Run(() => {
                SqlParameter[] param = { new SqlParameter("Id", Id) };
                try
                {
                    DataSql.ScalarCommand("StaffDel", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static List<Staff> ConvertList(List<object[]> listOb)
        {
            List<Staff> list = new List<Staff>();
            for (int i = 0; i < listOb.Count; i++)
            {
                list.Add((Staff)listOb[i]);
            }
            return list;
        }

        public static string[] ArrSexList = { "не указан", "муж", "жен" };
    }

}