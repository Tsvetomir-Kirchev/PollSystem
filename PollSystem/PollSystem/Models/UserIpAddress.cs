using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollSystem.Models
{
    public class UserIpAddress
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public string IpAddress { get; set; }
    }
}