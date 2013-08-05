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

        public ActionResult AllPolls()
        {
            return View();
        }

        public ActionResult Vote()
        {
            return View();
        }

    }
}
