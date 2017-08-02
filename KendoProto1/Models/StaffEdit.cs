using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KendoProto1.Models
{
    public class StaffEdit
    {
        [Display(Name = "ИД")]
        public int Id { get; set; }
        [Display(Name = "Ф.И.О.")]
        public string StaffName { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }
        [Display(Name = "К-во")]
        public int Qty { get; set; }
        [Display(Name = "Площадь")]
        public double Square { get; set; }
        [Display(Name = "Админ")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Страна-1")]
        public int Country1 { get; set; }
        [Display(Name = "Страна-2")]
        public int Country2 { get; set; }
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Описание")]
        public string Deskr { get; set; }
        [Display(Name = "Картинка")]
        public string Foto { get; set; }
    }
}