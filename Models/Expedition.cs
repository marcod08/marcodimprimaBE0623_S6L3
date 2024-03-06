using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace S6L1.Models
{
    public class Expedition
    {
        
        public int Id { get; set; }
        public int IdPrivateUser { get; set; }
        public int IdCompanyUser { get; set; }
        public string SellerName { get; set; }
        public DateTime ExpeditionStartDate { get; set; }
        public double Weight { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ClientName { get; set; }
        public double ExpeditionPrice { get; set; }
        public DateTime ExpeditionEndDate {  get; set; }
    }
}