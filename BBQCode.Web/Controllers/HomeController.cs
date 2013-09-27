using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBQCode.Web.Models;
using BBQCode.Web.Helpers;
namespace BBQCode.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Coop()
        {
            return View(Application.Members().OrderBy(x => Guid.NewGuid()).ToList());
        }


        public ActionResult Projects()
        {   
            return View(Application.Projects());
        }

        public ActionResult Project(string id)
        {
            if (!Application.Projects().ContainsKey(id))
                return RedirectToAction("Projects");

            return View(Application.Projects()[id]);
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            var c = new ContactUsModel();
            var r = new Random();
            c.QuestionPartOne = r.Next(1, 5);
            c.QuestionPartTwo = r.Next(1, 5);
            return View(c);
        }

        [HttpPost]
        public bool ContactUs(ContactUsModel model)
        {
            var valid = true;
            if (model.QuestionPartOne + model.QuestionPartTwo != model.Answer) valid = false;
            if (!String.IsNullOrWhiteSpace(model.DoNotFillThis)) valid = false;
            if (valid) valid = Email.SendContactUs(model.FullName, model.Email, model.Message);
            return valid;
        }
    }
}
