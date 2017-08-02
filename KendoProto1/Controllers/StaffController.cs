using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoProto1.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KendoProto1.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        public string GetSex()
        {
            return JsonConvert.SerializeObject(StaffCrud.ArrSexList);
        }

        public JsonResult Read(/*int skip, int take*/)
        {
            var list =  StaffCrud.GetAll() as IEnumerable<Staff>;
            //   list = list.Skip(skip).Take(take);

            return this.Jsonp(list);
        }

        public async Task<ActionResult> Create()
        {
            List<StaffEdit> list = this.DeserializeObject<IEnumerable<StaffEdit>>("models") as List<StaffEdit>;
            List<Staff> list2 = new List<Staff>();

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    int Id = await StaffCrud.Add((Staff)list[i]);
                    list[i].Id = Id;
                    Staff NewStaff = await StaffCrud.GetById(Id);
                    list2.Add(NewStaff);
                }
            }

            return this.Jsonp(list2);
        }

        public async Task<JsonResult> Update()
        {
            List<Staff> list = this.DeserializeObject<IEnumerable<Staff>>("models") as List<Staff>;

            List<Staff> list2 = new List<Staff>();

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    await StaffCrud.Edit(list[i]);
                    Staff model = await StaffCrud.GetById(list[i].Id);
                    list2.Add(model);
                }
            }
            var list3 = list2 as IEnumerable<Staff>;
            return this.Jsonp(list3);
        }

        public async Task<JsonResult> Destroy()
        {
            List<StaffEdit> list = this.DeserializeObject<IEnumerable<StaffEdit>>("models") as List<StaffEdit>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    await StaffCrud.Del(list[i].Id);
                }
            }
            return this.Jsonp(list);
        }

        [HttpPost]
        public async Task Delete(int Id)
        {
           await  StaffCrud.Del(Id);
        }

        [HttpPost]
        public async Task Edit(object model = null)
        {
            Staff country = (Staff)model;
            try
            {
                await StaffCrud.Edit(country);
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }
        }
    }
}