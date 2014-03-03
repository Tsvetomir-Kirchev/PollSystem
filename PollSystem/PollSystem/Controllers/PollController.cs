using PollSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollSystem.Controllers
{
    public class PollController : Controller
    {
        //
        // GET: /Poll/

        private PollSystemContext _db;

        public PollController()
        {
            _db = new PollSystemContext();
        }

        public ActionResult AllPolls()
        {
            List<Poll> polls = _db.Polls.ToList();

            return View(polls);
        }

        [HttpGet]
        public ActionResult Vote(int id)
        {
            var poll = _db.Polls.Find(id);

            return View(poll);
        }

        [HttpPost]
        public ActionResult CreateVote(int id, int pollId)
        {
            _db.Polls.Find(pollId).Votes.Add(new Vote { DateVoted = DateTime.Now, AnswerId = id });
            _db.SaveChanges();

            return View();
        }

    }
}
