using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;
            string message = "";

            using (var reader = new System.IO.StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch
                {
                    message = "Error";
                }
            }

            var database = Sitecore.Configuration.Factory.GetDatabase("master");

            var parentItem = database.GetItem(parentPath);

            var templateID = new TemplateID(new ID("{724BC583-2DAE-40FE-AEF0-9901AA91F476}"));


            int CountAdded = 0;
            int CountEdited = 0;

            using (new SecurityDisabler())
            {
                foreach (var e in events)
                {
                    var name = ItemUtil.ProposeValidItemName(e.ContentHeading);
                                        
                    Item item = parentItem.GetChildren().FirstOrDefault(a => a.Name == name);

                    if (item != null)
                    {
                        // 12.3 optional
                        var id = item.ID;
                        item.Versions.AddVersion();
                        item = database.GetItem(id);

                        CountEdited++;
                    }
                    else
                    {
                        item = parentItem.Add(name, templateID);
                        CountAdded++;
                    }

                    item.Editing.BeginEdit();

                    if (item != null && item.State != null) // && !item.State.GetWorkflowState().FinalState)
                    {
                        item[FieldIDs.Workflow] = "{4F39DC00-3194-4F35-BE0A-FA4CCCDE7A37}";
                        item[FieldIDs.WorkflowState] = "{89AE0EB4-A3B4-43F3-A6B7-05049C3A5411}";
                    }
                    item["ContentHeading"] = e.ContentHeading;
                    item["ContentIntro"] = e.ContentIntro;
                    item["Difficulty"] = e.Difficulty;
                    item["Duration"] = e.Duration;
                    item["Highlights"] = e.Highlights;
                    item["StartDate"] = DateUtil.ToIsoDate(e.StartDate);

                    item.Editing.EndEdit();

                }
            }

            TempData["results"] = string.Format("{0} items edited; {1} items added", CountEdited.ToString(), CountAdded.ToString());
            TempData["message"] = message;

            return View();
        }
    }
}