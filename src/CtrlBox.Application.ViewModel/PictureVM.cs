
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
    }
}
