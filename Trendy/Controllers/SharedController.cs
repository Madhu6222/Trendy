using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Trendy.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage() 
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;


            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/img/UploadedImages"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageURL = string.Format("/img/UploadedImages/{0}",fileName) };

                    //var newImage = new Image() { Name = fileName };

                    //if (ImagesService.Instance.SaveNewImage(newImage))
                    //{
                    //    result.Data = new { Success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                    //}
                    //else
                    //{
                    //    result.Data = new { Success = false, Message = new HttpStatusCodeResult(500) };
                    //}
                }
            catch (Exception ex) 
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }
    }
}