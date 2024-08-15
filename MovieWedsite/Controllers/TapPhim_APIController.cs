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
    public class TapPhim_APIController : ApiController
    {
        MovieWebsiteEntities db = new MovieWebsiteEntities();

        public async Task<IHttpActionResult> insert_tapphim(string maPhim)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return StatusCode(HttpStatusCode.UnsupportedMediaType);

            var root = HttpContext.Current.Server.MapPath("~/Video");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi đọc phần thân MIME multipart: {ex.Message}");
            }

            // Tạo số ngẫu nhiên cho MaTapPhim
            Random random = new Random();
            int randomMaTapPhim = random.Next(0, 999);

            var phim = new TapPhim()
            {
                MaTapPhim = randomMaTapPhim.ToString(),
                MaPhim = maPhim,  // Sử dụng giá trị maPhim được truyền vào
                TenTapPhim = provider.FormData["TenTapPhim"],
            };

            var fileData = provider.FileData.FirstOrDefault();
            if (fileData != null)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));
                var filePath = Path.Combine(root, fileName);

                // Kiểm tra và đổi tên file nếu file đã tồn tại
                if (File.Exists(filePath))
                {
                    return Content(HttpStatusCode.Conflict, "File đã tồn tại. Vui lòng cung cấp tên khác.");
                }
                else
                {
                    phim.filePhim = fileName;
                }

                File.Move(fileData.LocalFileName, filePath);
            }

            try
            {
                db.TapPhims.Add(phim);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " Các lỗi xác thực là: ", fullErrorMessage);

                return BadRequest(exceptionMessage);
            }

            return Ok(phim);
        }

        [HttpPut]
        public async Task<IHttpActionResult> update_tapphim(string maTapPhim)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            var root = HttpContext.Current.Server.MapPath("~/Video");
            var provider = new MultipartFormDataStreamProvider(root);
            await Request.Content.ReadAsMultipartAsync(provider);
            var phim = new TapPhim()
            {
                MaTapPhim = provider.FormData["maTapPhim"],
                MaPhim = provider.FormData["maPhim"],  
                TenTapPhim = provider.FormData["TenTapPhim"],
            };
            var fileData = provider.FileData.FirstOrDefault();
            if (fileData != null)
            {
                var fileName = Path.GetFileName(fileData.Headers.ContentDisposition.FileName.Trim('"'));
                var filePath = Path.Combine(root, fileName);

                if (File.Exists(filePath))
                {
                    return Content(HttpStatusCode.Conflict, "File đã tồn tại. Vui lòng cung cấp tên khác.");
                }
                else
                {
                    phim.filePhim = fileName;
                }

                File.Move(fileData.LocalFileName, filePath);
            }
            db.TapPhims.Add(phim);
            db.SaveChanges();
            return Ok(phim);
        }
    }

}
