using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KendoProto1.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KendoProto1.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> GetListAsync()
        {
            List<Country> list = await CountryCrud.GetAll();

            return JsonConvert.SerializeObject(list);
        }

        public async Task<JsonResult> Read(/*int skip, int take*/)
        {
            var list = await CountryCrud.GetAll() as IEnumerable<Country>;

            //   list = list.Skip(skip).Take(take);

            return this.Jsonp(list);
        }

        public async Task<ActionResult> Create()
        {
            List<Country> list = this.DeserializeObject<IEnumerable<Country>>("models") as List<Country>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    int Id = await CountryCrud.Add(list[i]);
                    list[i].Id = Id;
                }
            }
            return this.Jsonp(list);
        }

        public async Task<JsonResult> Update()
        {
            List<Country> list = this.DeserializeObject<IEnumerable<Country>>("models") as List<Country>;

            if (list != null)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    await CountryCrud.Edit(list[i]);
                }
            }
            return this.Jsonp(list);
        }

        public async Task<JsonResult> Destroy()
        {
            List<Country> list = this.DeserializeObject<IEnumerable<Country>>("models") as List<Country>;

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
            await CountryCrud.Del(Id);
        }

        [HttpPost]
        public async Task Edit(object model = null)
        {
            Country country = (Country)model;
            try
            {
                await CountryCrud.Edit(country);
            }
            catch(Exception ex)
            {
                string strError = ex.Message;
            }
        }
    }
}