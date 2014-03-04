using PollSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PollSystem.Controllers
{
    public class PollController : Controller
    {
        public const int ITEMS_PER_PAGE = 3;
        private PollSystemContext _db;

        public PollController()
        {
            _db = new PollSystemContext();
        }

        public ActionResult AllPolls(int page = 1)
        {
            List<Poll> polls = _db.Polls.OrderBy(p => p.Id).Skip(ITEMS_PER_PAGE * (page - 1)).Take(ITEMS_PER_PAGE).ToList();
            ViewBag.Page = page;
            ViewBag.AllPages = _db.Polls.Count() / ITEMS_PER_PAGE + 1;

            return View(polls);
        }

        [HttpGet]
        public ActionResult Vote(int id, int page = 1)
        {
            var poll = _db.Polls.Find(id);
            ViewBag.Page = page;

            return View(poll);
        }

        [HttpPost]
        public ActionResult CreateVote(int id, int pollId)
        {
            var ip = Request.ServerVariables["REMOTE_ADDR"];
            var poll = _db.Polls.Find(pollId);
            if (poll.UserIpAddress != null && poll.UserIpAddress.Equals(ip))
            {
                ViewBag.Title = "Sorry, you have been voted for this question already";
                ViewBag.Message = "Sorry, you can vote only once for this question with this IP address.";
                return PartialView("_Dialog");
            }

            _db.Polls.Find(pollId).Votes.Add(new Vote { DateVoted = DateTime.Now, AnswerId = id });
            poll.UserIpAddress = ip;
            _db.Entry(poll).State = EntityState.Modified;
            _db.SaveChanges();

            return View();
        }

        public PartialViewResult Dialog()
        {
            return PartialView("_Dialog");
        }

    }
}
