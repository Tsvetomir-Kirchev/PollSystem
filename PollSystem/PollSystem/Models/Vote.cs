using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollSystem.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public DateTime DateVoted { get; set; }
    }
}