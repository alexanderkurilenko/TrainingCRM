using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training.Portals.Models
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Roles Role { get; set; }
    }
}