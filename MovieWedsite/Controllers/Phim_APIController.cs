using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MovieWedsite.Models;

namespace MovieWedsite.Controllers
{
    public class Phim_APIController : ApiController
    {
        MovieWebsiteEntities db = new MovieWebsiteEntities();

        [HttpPost]
        public async Task<IHttpActionResult> InsertPhim()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return StatusCode(HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/IMG_MOVIE");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);

            var newPhim = new Phim
            {
                MaPhim = provider.FormData["MaPhim"],
                TenPhim = provider.FormData["TenPhim"],
                Mota = provider.FormData["Mota"],
                TacGia = provider.FormData["TacGia"],
                NamSanXuat = DateTime.Parse(provider.FormData["NamSanXuat"])
            };

            var fileData = provider.FileData.FirstOrDefault();
            if (fileData != null)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));
                var filePath = Path.Combine(root, fileName);

                // Kiểm tra và đổi tên file nếu file đã tồn tại
                if (File.Exists(filePath))
                {
                    return Content(HttpStatusCode.Conflict, "File already exists. Please provide a different name.");
                }
                else
                {
                    newPhim.Anhbia = fileName;
                }

                File.Move(fileData.LocalFileName, filePath);
            }
                db.Phims.Add(newPhim);
                db.SaveChanges();
            return Ok(newPhim);
        }

        [HttpDelete]
        public IHttpActionResult DeletedPhim(string maPhim)
        {
            var phims = db.Phims.Where(p => p.MaPhim.Contains(maPhim)).ToList();
            if (phims.Count == 0)
            {
                return NotFound();
            }

            foreach (var phim in phims)
            {
                db.Phims.Remove(phim);
            }

            db.SaveChanges();
            return Ok(phims);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update_PhimAsync(string maPhim)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return StatusCode(HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/IMG_MOVIE");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);
            var existingPhim = db.Phims.FirstOrDefault(p => p.MaPhim == maPhim);
            if (existingPhim == null)
            {
                return NotFound();
            }
            existingPhim.TenPhim = provider.FormData["TenPhim"];
            existingPhim.Mota = provider.FormData["Mota"];
            existingPhim.TacGia = provider.FormData["TacGia"];
            existingPhim.NamSanXuat = DateTime.Parse(provider.FormData["NamSanXuat"]);
            var fileData = provider.FileData.FirstOrDefault();
            if (fileData != null)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));
                var filePath = Path.Combine(root, fileName);

                // Kiểm tra và đổi tên file nếu file đã tồn tại
                if (File.Exists(filePath))
                {
                    return Content(HttpStatusCode.Conflict, "File already exists. Please provide a different name.");
                }
                else
                {
                    existingPhim.Anhbia = fileName;
                }

                File.Move(fileData.LocalFileName, filePath);
            }
            db.SaveChanges();

            return Ok(existingPhim);
        }
        

        [HttpPost]
        public IHttpActionResult IncrementViewCount(string maPhim)
        {
            var phim = db.Phims.Find(maPhim);
            if (phim == null)
            {
                return NotFound();
            }

            phim.LuotXem += 1;
            db.SaveChanges();

            return Ok(new { message = "View count increased", view_count = phim.LuotXem });
        }

    }
}
