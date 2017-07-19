using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Links;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace events.tac.local.Controllers
{
    public class BreadcrumbController : Controller
    {
        // GET: Breadcrumb
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        private IEnumerable<NavigationItem> CreateModel()
        {
            var currentItem = RenderingContext.Current.ContextItem;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var breadcrumb = RenderingContext.Current.ContextItem.Axes.GetAncestors()
                .Where(a => a.Axes.IsDescendantOf(homeItem)
                    && a["ExcludeFromNavigation"] != "1")
                .Concat(new Item[] { currentItem })
                .ToList();

            IEnumerable<NavigationItem> NavigationList = breadcrumb.Select(s => new NavigationItem
            {
                Title = s.Fields["ContentHeading"].ToString(),
                URL = LinkManager.GetItemUrl(s),
                Active = (s.ID == currentItem.ID)
            });
            return NavigationList;
        }
    }
}