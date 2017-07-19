using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using events.tac.local.Business.Navigation;
using TAC.Utils.SitecoreModels;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using TAC.Utils.Abstractions;
using TAC.Utils.TestModels;

namespace events.UnitTests
{
    [TestClass]
    public class NavigationModelBuilderTests
    {
               
        // Lab 14.2

        [TestMethod]
        public void ReturnsAModel()
        {
            NavigationModelBuilder nmb = new NavigationModelBuilder();
            TestItem _item = new TestItem("test");

            var result = nmb.CreateNavigationMenu(_item, _item);

            Assert.IsNotNull(result);
        }
    }
}
