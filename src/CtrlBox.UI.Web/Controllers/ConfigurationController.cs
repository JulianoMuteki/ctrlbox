using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;

namespace CtrlBox.UI.Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationApplicationService _configurationApplicationService;

        public ConfigurationController(IConfigurationApplicationService configurationApplicationService)
        {
            _configurationApplicationService = configurationApplicationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<string> iamgeIds = _configurationApplicationService.GetAll().OrderBy(x => x.CreationDate).Select(x => x.DT_RowId).ToList();
            return View(iamgeIds);
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
                    Name = uploadedImage.FileName.Substring(0, uploadedImage.FileName.LastIndexOf(".")),
                    Data = ms.ToArray(),
                    Width = image.Width,
                    Height = image.Height,
                    ContentType = uploadedImage.ContentType
                };

                _configurationApplicationService.Add(imageEntity);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewImage(Guid id)
        {

                PictureVM image = _configurationApplicationService.GetById(id);

                MemoryStream ms = new MemoryStream(image.Data);

                return new FileStreamResult(ms, image.ContentType);
            
        }
    }
}