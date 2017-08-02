using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KendoProto1.Models
{
    public class GridSettingCrud
    {
        public static Task<string> GridSettingGet (int UserId, string FormName, string GridName)
        {


            string strValueSeting = "";

            return Task.Run(() =>
            {

                SqlParameter[] param = { new SqlParameter("UserId", UserId),
                                     new SqlParameter("FormName", FormName),
                                     new SqlParameter("GridName", GridName) };

                object ob = DataSql.ScalarCommand("GridSettingGet", param);

                strValueSeting = ob.ToString();
                return strValueSeting;
            });
        }

        public static void GridSettingAddOrUpd(int UserId, string FormName, string GridName, string ValueSetting)
        {
            Task.Run(() => {
                SqlParameter[] param = {
                    new SqlParameter("UserId", UserId),
                    new SqlParameter("FormName", FormName),
                    new SqlParameter("GridName", GridName),
                    new SqlParameter("ValueSetting", ValueSetting) };
                try
                {
                    DataSql.ScalarCommand("GridSettingAddOrUpd", param);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            });
        }
    }
}