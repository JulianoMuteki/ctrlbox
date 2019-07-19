
using System;

namespace CtrlBox.Application.ViewModel
{
    public class PictureVM : ViewModelBase
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public string Base64Image { get { return GetBase64StringForImage(this.Data); } }

        private string GetBase64StringForImage(byte[] imageBytes)
        {
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
