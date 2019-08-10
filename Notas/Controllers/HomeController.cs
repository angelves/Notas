using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notas.Database.Logic;
using Notas.Models;
using Newtonsoft.Json;

namespace Notas.Controllers
{
    public class HomeController : Controller
    {
        private DbManagement db;

        public HomeController(DbManagement db)
        {
            this.db = db;

            db.CheckStart();
        }

        public IActionResult Index()
        {
            List<SelectListItem> query = db.GetNotasL();

            return View
            ( 
                new IndexModel
                {
                      NotasL = query
                    , SelectedNota = db.GetNota( int.Parse( query.Where(x => x.Selected).Single().Value ))
                }
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNota(IndexModel model)
        {
            db.SaveNota(model.SelectedNota);

            List<SelectListItem> query = db.GetNotasL();

            query.ForEach(x => x.Selected = false);
            query.Where(x => x.Value == model.SelectedNota.Id.ToString()).Single().Selected = true;

            return View
            (   "Index", new IndexModel
                {
                      NotasL = query
                    , SelectedNota = db.GetNota(int.Parse(query.Where(x => x.Selected).Single().Value))
                }
            );
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public JsonResult GetNota(int? Id)
        {
            return Json(JsonConvert.SerializeObject(db.GetNota((int)Id)));
        }


        [HttpPost]
        public IActionResult AddNota()
        {
            var id = db.AddNota();

            List<SelectListItem> query = db.GetNotasL();

            query.ForEach(x => x.Selected = false);
            query.Where(x => x.Value == id.ToString()).Single().Selected = true;

            return View
            ("Index", new IndexModel
            {
                NotasL = query
                , SelectedNota = db.GetNota(int.Parse(query.Where(x => x.Selected).Single().Value))
            }
            );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DelNota(IndexModel model)
        {

            db.DeleteNota(model.SelectedNota);

            return RedirectToAction("Index");

        }


    }
}
