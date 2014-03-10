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

        public ActionResult AllPolls(int page = 1, bool success = false)
        {
            List<Poll> polls = _db.Polls.OrderBy(p => p.Id).Skip(ITEMS_PER_PAGE * (page - 1)).Take(ITEMS_PER_PAGE).ToList();
            ViewBag.Page = page;
            int allPolls = _db.Polls.Count();
            if (allPolls % ITEMS_PER_PAGE == 0)
            {
                ViewBag.AllPages = allPolls / ITEMS_PER_PAGE;
            }
            else
            {
                ViewBag.AllPages = allPolls / ITEMS_PER_PAGE + 1;
            }

            if (page > ViewBag.AllPages || page < 1)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound, "Bad Request");
            }

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
        public ActionResult CreateVote(int id, int pollId, int page)
        {
            var ip = Request.ServerVariables["REMOTE_ADDR"];
            var poll = _db.Polls.Find(pollId);

            Vote voted = (from votes in _db.Votes
                        .Where(v => v.PollId == pollId)
                        .Where(v => v.UserIp == ip)
                        select votes).FirstOrDefault();

            if (voted != null)
            {
                return Json(new { showDialog = true });
            }

            _db.Polls.Find(pollId).Votes.Add(new Vote { DateVoted = DateTime.Now, AnswerId = id, PollId = pollId, UserIp = ip });
            _db.Entry(poll).State = EntityState.Modified;
            _db.SaveChanges();

            var applicationPath = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;

            // TODO: Fix routings
            return Json(new { url = applicationPath + "/Poll/AllPolls/?page=" + page });
        }

        public PartialViewResult Dialog()
        {
            return PartialView("_Dialog");
        }

    }
}
