using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWedsite.Models;

namespace MovieWedsite.Controllers
{
    public class TapPhimController : Controller
    {
        // GET: TapPhim

        MovieWebsiteEntities db = new MovieWebsiteEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult show_TapPhim(string id)
        {
            List<TapPhim> tapPhims = db.TapPhims.Where(p => p.Phim.MaPhim.Contains(id)).OrderBy(p=>p.filePhim).ToList();
            return View(tapPhims);

        }

        public ActionResult insert_tapPhim()
        {
            return View();
        }

        public ActionResult update_tapPhim()
        {
            return View();
        }


    }
}