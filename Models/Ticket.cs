
using System;

namespace RoboIAZoho.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
