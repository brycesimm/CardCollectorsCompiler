using CardCollectorsCompiler.Models;
using CardCollectorsCompiler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

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

        public ActionResult AddCard()
        {
            ViewBag.Sets = CCCcontext.Sets.OrderBy(x => x.Year).ThenBy(x => (x.Name + " " + x.Language)).ToList();
            ViewBag.Holos = CCCcontext.Holos.OrderBy(x => x.Id).ToList();
            return View("AddCard");
        }

        public ActionResult EditCard(int Id)
        {
            var card = CCCcontext.Cards.FirstOrDefault(x => x.Id == Id);
            ViewBag.Sets = CCCcontext.Sets.OrderBy(x => x.Year).ThenBy(x => x.Name).ToList();
            ViewBag.Holos = CCCcontext.Holos.OrderBy(x => x.Id).ToList();
            if (card != null)
            {
                ViewBag.Set = CCCcontext.Sets.FirstOrDefault(x => x.Id == card.SetId);
                ViewBag.Holo = CCCcontext.Holos.FirstOrDefault(x => x.Id == card.HoloId);
            }
            else
            {
                ViewBag.Set = CCCcontext.Sets.FirstOrDefault();
                ViewBag.Holo = CCCcontext.Holos.FirstOrDefault();
            }
            return View(card);
        }

        public ActionResult DeleteCard(int Id)
        {
            var card = CCCcontext.Cards.FirstOrDefault(x => x.Id == Id);
            if (card != null)
            {
                CCCcontext.Remove(card);
                CCCcontext.SaveChanges();
            }

            return ViewCards();
        }

        [HttpPost]
        public ActionResult SaveCard(Card card)
        {
            if (ModelState.IsValid)
            {
                if (card.Id == null || card.Id == 0)
                {
                    var similarRecords = CCCcontext.Cards.Where(x => x.Name == card.Name && x.SetId == card.SetId && x.Number == card.Number && x.Edition == card.Edition).ToList();

                    if (similarRecords.Count > 0)
                    {
                        return View("Admin");
                    }
                    else
                    {
                        CCCcontext.Cards.Add(card);
                    }
                }
                else
                {
                    CCCcontext.Cards.Update(card);
                }

                CCCcontext.SaveChanges();

                return View("Admin");
            }
            return View();
        }

        public ActionResult ViewCards()
        {
            var cards = CCCcontext.Cards.OrderBy(x => x.SetId).ThenBy(x => x.Number).ThenBy(x => x.Name).ToList();
            return View("Viewcards", cards);
        }
    }
}
