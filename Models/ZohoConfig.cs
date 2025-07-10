
using System;

namespace RoboIAZoho.Models
{
    public class ZohoConfig
    {
        public int Id { get; set; }
        public string ApiType { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
