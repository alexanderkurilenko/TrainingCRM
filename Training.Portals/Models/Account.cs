using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Xrm.Sdk;

namespace Training.Portals.Models
{
    public class Account
    {
        public Guid AccountID { get; set; }
        public string AccountName { get; set; }
        public int NumberOfEmployees { get; set; }
        public Money Revenue { get; set; }
        public EntityReference PrimaryContact { get; set; }
        public string PrimaryContactName { get; set; }
        public decimal RevenueValue { get; set; }
    }
}