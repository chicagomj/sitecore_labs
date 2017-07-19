using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Data.Items;
using Sitecore.Links;
using events.tac.local.Business.Navigation;
using TAC.Utils.SitecoreModels;

namespace events.tac.local.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation

        private readonly RenderingContext _currentContext;
        private readonly NavigationModelBuilder _modelBuilder;
        public NavigationController(RenderingContext context, NavigationModelBuilder modelBuilder)
        {
            _currentContext = context;
            _modelBuilder = modelBuilder;
        }
        
        public ActionResult Index()
        {
            Item current = _currentContext.ContextItem;
            Item section = current.Axes.GetAncestors()
                .FirstOrDefault(a => a.TemplateName == "Events Section");

            var model = _modelBuilder.CreateNavigationMenu(new SitecoreItem(section), new SitecoreItem(current));
            return View(model);
        }
        
    }    
}