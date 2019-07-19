using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using CtrlBox.Application.ViewModel;

namespace CtrlBox.UI.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        IList<PictureVM> _images = new List<PictureVM>();
        public ConfigurationController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            //using (ImageDBContext dbContext = new ImageDBContext())
            //{
           // List<Guid> iamgeIds = _images.Select(m => m.Id).ToList();
            return View();
            //}
        }

        [HttpPost]
        public IActionResult UploadImage(IList<IFormFile> files)
        {
            IFormFile uploadedImage = files.FirstOrDefault();
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                uploadedImage.OpenReadStream().CopyTo(ms);

                Image image = Image.FromStream(ms);

                PictureVM imageEntity = new PictureVM()
                {
                  //  DT_RowId = Guid.NewGuid(),
                    Name = uploadedImage.Name,
                    Data = ms.ToArray(),
                    Width = image.Width,
                    Height = image.Height,
                    ContentType = uploadedImage.ContentType
                };

                _images.Add(imageEntity);



            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewImage(Guid id)
        {

                PictureVM image = _images.FirstOrDefault(m => m.DT_RowId == id.ToString());

                MemoryStream ms = new MemoryStream(image.Data);

                return new FileStreamResult(ms, image.ContentType);
            
        }
    }
}