using CardCollectorsCompiler.Models;
using CardCollectorsCompiler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardCollectorsCompiler.Controllers
{
    public class AdminController : Controller
    {
        private CCCDbContext CCCcontext;

        public AdminController(CCCDbContext context)
        {
            CCCcontext = context;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View("Admin");
        }

        public ActionResult AddSet()
        {
            ViewBag.Languages = CCCcontext.Languages.OrderBy(x => x.Value).ToList();
            return View("AddSet");
        }

        public ActionResult EditSet(int Id)
        {
            var set = CCCcontext.Sets.FirstOrDefault(x => x.Id == Id);
            ViewBag.Languages = CCCcontext.Languages.OrderBy(x => x.Value).ToList();
            return View(set);
        }

        public ActionResult DeleteSet(int Id)
        {
            var set = CCCcontext.Sets.FirstOrDefault(x => x.Id == Id);
            if(set != null)
            {
                CCCcontext.Remove(set);
                CCCcontext.SaveChanges();
            }

            return ViewSets();
        }

        [HttpPost]
        public ActionResult SaveSet(Set set)
        {
            if (ModelState.IsValid)
            {
                if(set.Id == null || set.Id == 0)
                {
                    var similarRecords = CCCcontext.Sets.Where(x => x.Name == set.Name && x.Language == set.Language && x.Year == set.Year && x.Count == set.Count).ToList();
                    
                    if (similarRecords.Count > 0)
                    {
                        return View("Admin");
                    }
                    else
                    {
                        CCCcontext.Sets.Add(set);
                    }
                }
                else
                {
                    CCCcontext.Sets.Update(set);
                }

                CCCcontext.SaveChanges();

                return View("Admin");
            }
            return View();
        }

        public ActionResult ViewSets()
        {
            var sets = CCCcontext.Sets.OrderBy(x => x.Name).ThenBy(x => x.Language).ToList();
            return View("ViewSets",sets);
        }
    }
}
