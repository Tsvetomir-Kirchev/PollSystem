﻿using PollSystem.Models;
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
            if (_db.Polls.Count() % 2 == 0)
            {
                ViewBag.AllPages = _db.Polls.Count() / ITEMS_PER_PAGE;
            }
            else
            {
                ViewBag.AllPages = _db.Polls.Count() / ITEMS_PER_PAGE + 1;
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
                return Content(CreateModal("You hvae voted this question",
                    "Sorry, you can vote only once for this question with this IP address"));
            }

            //if (UserIpAddress poll.UserIpAddress != null && poll.UserIpAddress.Equals(ip))
            //{
            //    return Content(CreateModal("You hvae voted this question",
            //        "Sorry, you can vote only once for this question with this IP address"));
            //}

            _db.Polls.Find(pollId).Votes.Add(new Vote { DateVoted = DateTime.Now, AnswerId = id, PollId = pollId, UserIp = ip });
            //poll.UserIpAddress.Add(ip.ToString());
            _db.Entry(poll).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("AllPolls", new { page = page });
        }

        public PartialViewResult Dialog()
        {
            return PartialView("_Dialog");
        }

        private string CreateModal(string title, string msg)
        {
            string appPath = HttpContext.Request.ApplicationPath;
            string dialog = "<link rel='stylesheet' href='//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css'>" +
                "<script src='//code.jquery.com/jquery-1.9.1.js'></script>" +
                "<script src='//code.jquery.com/ui/1.10.4/jquery-ui.js'></script>" +
                "<div id='dialog' style='background: #1f2f2f; color: #fff;' title='" + title + "'>" +
                "<p>" + msg + "</p>" +
                "</div>" +
                "<script>" +
                "$(function () { $('#dialog').dialog({ resizable: false, height: 240," +
                "buttons: {'OK': function () {$(this).dialog('close'); window.location = '" + appPath + "/'} }});});" +
                "</script>";
            
            return dialog;
        }

    }
}
