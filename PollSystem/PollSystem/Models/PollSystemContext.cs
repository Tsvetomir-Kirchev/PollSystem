using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PollSystem.Models
{
    public class PollSystemContext : DbContext
    {
        public PollSystemContext()
            : base("EFDbContext")
        {

        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}