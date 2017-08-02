using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using KendoProto1.Models;
using System.ComponentModel.DataAnnotations;

namespace KendoProto1.Models
{
    public class Country
    {
        [Display(Name = "ИД")]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string CountryName { get; set; }

        public Country()
        {
            Id = 0;
            CountryName = "";
        }

        public static explicit operator Country(object[] objects)
        {
            return new Country
            {
                Id = int.Parse(objects[0].ToString()),
                CountryName = objects[1].ToString()
            };
        }

        public override string ToString()
        {
            return CountryName;
        }
        #region  перегруженные операторы

        public bool Equals(Country other)
        {
            return !ReferenceEquals(null, other)
        && (ReferenceEquals(this, other) ||
        ((other.Id == Id)
        && (other.CountryName == CountryName)));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Country);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Country left, Country right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Country left, Country right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}