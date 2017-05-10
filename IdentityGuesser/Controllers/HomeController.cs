using System.Web.Mvc;
using System.Diagnostics;
using fw8.CognitiveServices;
using System.IO;
using System.Web;
using fw8.CognitiveServices.Tags;
using System;

namespace IdentityGuesser.Controllers
{
    public class HomeController : Controller
    {
        //Displaying two pages
        public ActionResult Index()
        {
            return View();
        }
        
        //Getting images from an URL
        [HttpPost]
        public ActionResult ImagefromLink(PoemModel model)
        {
            if (ModelState.IsValid)
            {
                Manager instance = new Manager();
                string strname = model.Link;
                instance.ProcessingImage(strname);
            }
            return View("Index");
        }
        //Getting image from an upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                Manager instance = new Manager();
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                file.SaveAs(path);
                instance.ProcessingImage(path);
            }
            return View("Index");
            //TESTING
        }














    }
}