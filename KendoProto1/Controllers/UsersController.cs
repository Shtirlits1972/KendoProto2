using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoProto1.Models;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

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

        public async Task<JsonResult> GetAll(/*int skip, int take*/)
        {
            var list = await UsersCrud.GetAll() as IEnumerable<Users>;

            //   list = list.Skip(skip).Take(take);

            return this.Jsonp(list);
        }

        public ActionResult Add()
        {
            List<Users> list = this.DeserializeObject<IEnumerable<Users>>("models") as List<Users>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    int Id = UsersCrud.Add(list[i]);
                    list[i].Id = Id;
                }
            }
            return this.Jsonp(list);
        }

        public JsonResult Edit()
        {
            List<Users> list = this.DeserializeObject<IEnumerable<Users>>("models") as List<Users>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                     UsersCrud.Edit(list[i]);
                }
            }
            return this.Jsonp(list);
        }

        public JsonResult Del()
        {
            List<Users> list = this.DeserializeObject<IEnumerable<Users>>("models") as List<Users>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    UsersCrud.Del(list[i].Id);
                }
            }
            return this.Jsonp(list);
        }

        public string RoleList2()
        {
            List<MyRole> list = new List<MyRole>();

            MyRole admin = new MyRole { name = "Админ" };
            list.Add(admin);

            MyRole user = new MyRole { name = "Юзер" };
            list.Add(user);

            MyRole user2 = new MyRole { name = "Юзер-2" };
            list.Add(user2);

            return JsonConvert.SerializeObject(list);
        }

        public IEnumerable RoleDrops()
        {
            List<MyRole> list = new List<MyRole>();

            MyRole admin = new MyRole {name= "Админ" };
            list.Add(admin);

            MyRole user = new MyRole { name = "Юзер" };
            list.Add(user);

            MyRole user2 = new MyRole { name = "Юзер-2" };
            list.Add(user2);

            IEnumerable<MyRole> list2 = list as IEnumerable<MyRole>;
            
             return list2;
        }

        [HttpPost]
        public void Delete(int Id){
            UsersCrud.Del(Id);
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

    public class MyRole
    {
      public  string name { get; set; }
    }
}
