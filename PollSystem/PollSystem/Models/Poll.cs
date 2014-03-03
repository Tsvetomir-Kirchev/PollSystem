using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollSystem.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual List<Answer> Answers { get; set; }
        public virtual List<Vote> Votes { get; set; }
    }
}