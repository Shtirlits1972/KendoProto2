using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using KendoProto1.Models;
using System.ComponentModel.DataAnnotations;

namespace KendoProto1.Models
{
    public class Staff
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
        public Country Country1 { get; set; }
        [Display(Name = "Страна-2")]
        public Country Country2 { get; set; }
        [Display(Name = "Пол")]
        public string Sex { get; set; }
        [Display(Name = "Описание")]
        public string Deskr { get; set; }
        [Display(Name = "Картинка")]
        public string Foto { get; set; }

        public Staff()
        {
            Id = 0;
            StaffName = "";
            Birthday = DateTime.Now;
            Qty = 0;
            Square = 0;
            IsAdmin = false;
            Country1 = new Country
            {
                Id = 0,
                CountryName = ""
            };
            Country2 = new Country
            {
                Id = 0,
                CountryName = ""
            };
            Sex = "";
            Deskr = "";
            Foto = "";
        }

        public static explicit operator Staff(StaffEdit model)
        {
            return new Staff
            {
                Id = model.Id,
                StaffName = model.StaffName,
                Birthday = model.Birthday,
                Qty = model.Qty,
                Square = model.Square,
                IsAdmin = model.IsAdmin,

                Country1 = new Country
                {
                    Id = model.Country1,
                    CountryName = ""
                },

                Country2 = new Country
                {
                    Id = model.Country2,
                    CountryName = ""
                },
                Sex = model.Sex,
                Deskr = model.Deskr,
                Foto = model.Foto
            };
        }

        public static explicit operator Staff(object[] objects)
        {
            return new Staff
            {
                Id = int.Parse(objects[0].ToString()),
                StaffName = objects[1].ToString(),
                Birthday = (DateTime)objects[2],
                Qty = int.Parse(objects[3].ToString()),
                Square = double.Parse(objects[4].ToString()),
                IsAdmin = bool.Parse(objects[5].ToString()),

                Country1 = new Country
                {
                    Id = int.Parse(objects[6].ToString()),
                    CountryName = objects[7].ToString()
                },

                Country2 = new Country
                {
                    Id = int.Parse(objects[8].ToString()),
                    CountryName = objects[9].ToString()
                },
                Sex = objects[10].ToString(),
                Deskr = objects[11].ToString(),
                Foto = objects[12].ToString()
            };
        }

        public override string ToString()
        {
            return StaffName;
        }
        #region  перегруженные операторы

        public bool Equals(Staff other)
        {
            return !ReferenceEquals(null, other)
        && (ReferenceEquals(this, other) ||
        ((other.Id == Id)
        && (other.StaffName == StaffName)
        && (other.Birthday == Birthday)
        && (other.Qty == Qty)
        && (other.Square == Square)
        && (other.IsAdmin == IsAdmin)
        && (other.Country1 == Country1)
        && (other.Country2 == Country2)
        && (other.Sex == Sex)
        && (other.Deskr == Deskr)
        && (other.Foto == Foto)));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Staff);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Staff left, Staff right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Staff left, Staff right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}