using events.tac.local.Models;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.Mvc;
using Sitecore.Analytics.Outcome.Extensions;

namespace events.tac.local.Controllers
{
    public class SubscribeFormController : Controller
    {
        // GET: SubscribeForm
        public ActionResult Index()
        {         
            var item = Sitecore.Context.Database.GetItem("/sitecore/content/Events/Repository/Subscribe Intro Text");

            var pageIntro = new PageIntro()
            {
                Heading = new HtmlString(FieldRenderer.Render(item, "PageHeading")),
                Intro = new HtmlString(FieldRenderer.Render(item, "PageIntro"))
            };

            return View(pageIntro);
        }

        [HttpPost]
        [ValidateFormHandler]
        public ActionResult Index(string email)
        {
            // 10.3 Add entered email to session
            Sitecore.Analytics.Tracker.Current.Session.Identify(email);
            string message = "";

            var contact = Sitecore.Analytics.Tracker.Current.Contact;

            var emails = contact.GetFacet<IContactEmailAddresses>("Emails");

            if (!emails.Entries.Contains("personal"))
            {

                var personalEmail = emails.Entries.Create("personal");

                personalEmail.SmtpAddress = email;
                emails.Preferred = "personal";
                message = "Email Added; ";
            }
            
            // 10.4 Add Outcome Subscribed
            Sitecore.Data.ID outcomeID = new Sitecore.Data.ID("{C41E28D0-66C4-423F-84D5-09DEABD6294A}");
            Sitecore.Data.ID contactID = new Sitecore.Data.ID(contact.ContactId);
            
            var co = new Sitecore.Analytics.Outcome.Model.ContactOutcome(Sitecore.Data.ID.NewID, outcomeID, contactID);

            Sitecore.Analytics.Tracker.Current.RegisterContactOutcome(co);

            message += "Subscribed; ";

            // 10 optional
            Sitecore.Analytics.Data.PageEventData pageEvent = new Sitecore.Analytics.Data.PageEventData("Newsletter Signup");

            var page = Sitecore.Analytics.Tracker.Current.CurrentPage;

            page.Register(pageEvent);

            message += "Registered; ";

            TempData["message"] = message;

            return View("Confirmation");
        }
    }
}