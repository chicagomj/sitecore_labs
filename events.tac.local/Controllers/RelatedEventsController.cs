using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Fields;
using Sitecore.Links;
using events.tac.local.Models;


namespace events.tac.local.Controllers
{
    public class RelatedEventsController : Controller
    {
        // GET: RelatedEvents
        public ActionResult Index()
        {
            var contextItem = RenderingContext.Current.Rendering.Item;
            if (contextItem == null) return new EmptyResult();

            MultilistField relatedEvents = contextItem.Fields["Related Events"];
            if (relatedEvents == null) return new EmptyResult();

            var events = relatedEvents.GetItems().Select(a => new NavigationItem()
            {
                Title = a.DisplayName,
                URL = LinkManager.GetItemUrl(a)
            }
            );

            return View(events);
        }


    }
}