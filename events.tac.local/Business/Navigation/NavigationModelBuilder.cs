using events.tac.local.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAC.Utils.Abstractions;

namespace events.tac.local.Business.Navigation
{
    public class NavigationModelBuilder
    {
        public NavigationMenu CreateNavigationMenu(IItem root, IItem current)
        {
            NavigationMenu menu = new NavigationMenu()
            {
                Title = root.DisplayName,
                URL = root.GetUrl(), 
                // Children: if root is ancestor of current then return the Children of root, using a Select statement to project
                // the Item into a Navigation Menu invoking the CreateNavigationMenu; if root is not ancestor of current
                // return null.
                
                Children = root.IsAncestorOf(current) ?
                    root.GetChildren()
                    .Where(i => i["ExcludeFromNavigation"] != "1")
                    .Select(a => CreateNavigationMenu(a, current)) : null
            };

            return menu;
        }
    }
}