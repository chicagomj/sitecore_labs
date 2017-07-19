using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Controllers
{
    public class OverviewController : Controller
    {
        // GET: Overview
        public ActionResult Index()
        {
            var oList = new OverviewList();
            oList.ReadMore = "Read More";

            var contextItem = RenderingContext.Current.ContextItem;
            oList.AddRange(contextItem.GetChildren()
                .Select(a => new OverviewItem()
                {
                    Title = new HtmlString(a.DisplayName), // book shows fieldrenderer
                    URL = LinkManager.GetItemUrl(a),
                    Image = new HtmlString(FieldRenderer.Render(a, "decorationbanner", "mw=500&mh=333")),
                }));


            return View(oList);
        }
    }
}