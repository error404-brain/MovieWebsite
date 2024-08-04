using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWedsite.Models;

namespace MovieWedsite.Controllers
{
    public class TheLoaiPhimController : Controller
    {
        MovieWebsiteEntities db = new MovieWebsiteEntities();
        // GET: TheLoaiPhim
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult show_TheLoaiPhim()
        {
            List<theLoaiPhim> theloai = db.theLoaiPhims.ToList();
            return PartialView(theloai);
        }

        public ActionResult show_phim_the_Loai(string id)
        {
            List<phim_theloaiphim> tl = db.phim_theloaiphim.Where(p=>p.MaTheLoai.Contains(id)).ToList();
            return View(tl);
        }

    }
}