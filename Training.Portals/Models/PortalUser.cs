using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Training.Portals.Models
{
    public class PortalUser
    {
        public Guid PortalUSerId { get; set; }
        public string Login { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
    }
}