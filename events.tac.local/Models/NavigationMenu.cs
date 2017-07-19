using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using events.tac.local.Models;

namespace events.tac.local.Models
{
    public class NavigationMenu : NavigationItem 
    {
        public NavigationMenu()
        {
    
        }

        public IEnumerable<NavigationMenu> Children { get; set; }

    }
}