using CtrlBox.Application.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;

namespace CtrlBox.UI.Web.Helpers
{
    public static class GeneratePicture
    {
        public static PictureVM CreatePicture(IFormFile FilePicture, string name)
        {
            if (FilePicture == null || FilePicture.ContentType.ToLower().StartsWith("image/"))
            {
                MemoryStream ms = new MemoryStream();
                FilePicture.OpenReadStream().CopyTo(ms);

                Image image = Image.FromStream(ms);

                PictureVM imageEntity = new PictureVM()
                {
                    Name = name,
                    Data = ms.ToArray(),
                    Width = image.Width,
                    Height = image.Height,
                    ContentType = FilePicture.ContentType
                };

                return imageEntity;
            }
            return null;
        }

    }
}
