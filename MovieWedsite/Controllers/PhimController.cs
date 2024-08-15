using MovieWedsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieWedsite.Controllers
{
    public class PhimController : Controller
    {
        // GET: Phim
        MovieWebsiteEntities db = new MovieWebsiteEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult show_dsPhim_admin()
        {
            List<Phim> phim_lst = db.Phims.ToList();
            return View(phim_lst);
        }

        public PartialViewResult show_dsPhim_user()
        {
            List<Phim> phim_lst = db.Phims.ToList();
            return PartialView(phim_lst);
        }

        

        public ActionResult Deatail_Phim(string id)
        {
            Phim phim = db.Phims.Find(id);
            return View(phim);
        }
       
        public ActionResult find_Phim(string TenPhim)
        {
          
            List<Phim> phim = db.Phims.Where(p => p.TenPhim.Contains(TenPhim)).ToList();

            if (phim == null || !phim.Any())
            {
                Redirect("show_dsPhim_user");
            }

            return View(phim);
        }

        //insert using API_Phim
        public ActionResult insert_phim()
        {                    
            return View();
        }

        //delete phim ussing API_phim
        
        public ActionResult deleted_phim()
        {
            return View();
        }

        //update phim issing API
        public ActionResult update_phim()
        {
            return View();
        }


        

    }
}