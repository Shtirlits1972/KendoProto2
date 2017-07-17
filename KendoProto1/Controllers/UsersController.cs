using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using KendoProto1.Models;
using System.Threading.Tasks;

namespace KendoProto1.Controllers
{
    [Authorize]
    [Authorize(Roles = "Админ")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetUsers()
        {
            List<Users> list = await UsersCrud.GetAll();
            int page = 1;
            var data = list;
            var Data = new
            {
                page,
                total = Math.Ceiling((double)list.Count() / int.MaxValue),
                records = list.Count(),
                rows = data
            };

            return JsonConvert.SerializeObject(Data);
        }

        [HttpPost]
        public JsonResult Add(Users model)
        {
            try
            {
                UsersCrud.Add(model);
                return Json("Успех!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Edit(Users model)
        {
            try
            {
                UsersCrud.Edit(model);
                return Json("Успех!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                UsersCrud.Del(Id);
                return Json("Успех!");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public string RoleList(string SelName, bool IsFilter = false)
        {
            List<string> list = UsersCrud.GetRoles().ToList();

            if (IsFilter)
            {
                list.Insert(0, "Все");
            }

            string result = $"<select id='{SelName}' >";

            foreach (var item in list)
            {
                result = result + "<option value = '" + item + "'>" + item + "</option>";
            }

            result = result + "</select>";
            return result;
        }

        public string RoleFilter(string SelName)
        {
            List<string> list = UsersCrud.GetRoles().ToList();

            list.Insert(0, "Все");

            string result = $"<select id='{SelName}' >";

            result = result + "<option value = ''>" + list[0] + "</option>";
            if (list.Count > 0)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    result = result + "<option value = '" + list[i] + "'>" + list[i] + "</option>";
                }
            }

            result = result + "</select>";
            return result;
        }

    }
}
