using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoProto1.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string UserFio { get; set; }
        public bool Banned { get; set; }

        public override string ToString()
        {
            return UserFio;
        }

        public static explicit operator Users(object[] objects)
        {
            if (objects == null || objects.Length == 0)
            {
                return null;
            }
            else
            {
                return new Users
                {
                    Id = int.Parse(objects[0].ToString()),
                    Email = objects[1].ToString(),
                    Password = objects[2].ToString(),
                    Role = objects[3].ToString(),
                    UserFio = objects[4].ToString(),
                    Banned = (bool)objects[5]
                };
            }
        }
    }
}