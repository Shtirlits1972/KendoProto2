using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
//using System.Web.WebPages.Html;

namespace KendoProto1.Models
{
    public static class CountryCrud
    {

        public static Task<List<Country>> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return ConvertList(DataSql.GetAll("CountrySel"));
            });
        }

        public static Task<List<Country>> GetAll()
        {
            return Task.Run(() =>
            {
                return ConvertList(DataSql.GetAll("CountrySel"));
            });
        }

        public static Task<Country> GetById(int Id)
        {
            return Task.Run(() =>
            {
                SqlParameter[] param = { new SqlParameter("Id", Id) };

                return (Country)DataSql.GetAll("CountryById", param)[0];
            });
        }

        public static async Task Edit(Country Country)
        {
            await Task.Run(() => {
                SqlParameter[] param = {
                        new SqlParameter("Id", Country.Id),
                        new SqlParameter("CountryName", Country.CountryName) };

                try
                {
                    DataSql.ScalarCommand("CountryUpd", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static Task<int> Add(Country Country)
        {
            return Task.Run(() => {
                SqlParameter[] param = {
                new SqlParameter("CountryName", Country.CountryName) };
                int res = 0;
                try
                {
                    object ob = DataSql.ScalarCommand("CountryIns ", param);
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
                    DataSql.ScalarCommand("CountryDel", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }

        public static List<Country> ConvertList(List<object[]> listOb)
        {
            List<Country> list = new List<Country>();
            for (int i = 0; i < listOb.Count; i++)
            {
                list.Add((Country)listOb[i]);
            }
            return list;
        }

        public static async Task <HtmlString> CountryList(this HtmlHelper html, string SelName = "CountryList", bool IsFilter = false)
        {
            List<Country> list = await GetAll();

            string multiple = "";

            if (IsFilter)
            {
                multiple += "multiple ='multiple' ";
            }

            string result = $" <select id='{SelName}' class='button btn-default' style='width: 100%;' {multiple} >";

            foreach (var item in list)
            {
                result = result + " <option value = '" + item.Id + "'>" + item.ToString() + "</option>";
            }

            result = result + " </select>";

            return new HtmlString(result);
        }

    }
}