using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.Office365.OAuth;

namespace CRMMetadata.Controllers
{
    public class CRMController : Controller
    {
        // GET: CRM
        public async Task<ActionResult> Index()
        {
            return View(new CreateViewModel()
            {
                id = Guid.NewGuid()
            });
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "tenantName,id")] CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Redirect(string.Format("/CRM/Generate/{0}/{1}",model.id,model.tenantName));
            }
            return View(model);
        }
        
        public async Task<ActionResult> Generate(Guid id, string subpath)
        {
                try
                {

                    var st = await CRMSample.GetMetadata(subpath);
                    var wk = await CRMSample.Workspace(subpath);
                    HttpContext.Cache.Add(id.ToString() + "_Metadata", st, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    HttpContext.Cache.Add(id.ToString() + "_Workspace", wk, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    ViewBag.Message =
                        "Successfully Generated Anonymous CRM Metadata endpoint";
                    ViewBag.Url = string.Format("https://{0}/CRM/Metadata/{1}", HttpContext.Request.Url.Host, id);
                    return View();

                }
                catch (RedirectRequiredException ex)
                {
                    return Redirect(ex.RedirectUri.ToString());
                }
            
            
        }

        public async Task<ActionResult> Metadata(string id, string subpath)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index");
                Response.Headers.Add("Content-Type", "application/xml;charset=utf-8");
                if (!string.IsNullOrEmpty(subpath) &&
                    string.Equals(subpath, "$metadata", StringComparison.InvariantCultureIgnoreCase))
                {
                    Response.Write(HttpContext.Cache[id + "_Metadata"]);    
                }
                else
                {
                    Response.Write(HttpContext.Cache[id+"_Workspace"]);    
                }
                
                return new EmptyResult();
            }
            catch (RedirectRequiredException ex)
            {
                return Redirect(ex.RedirectUri.ToString());
            }
            
        }
    }
}