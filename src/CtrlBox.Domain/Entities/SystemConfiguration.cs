using CtrlBox.Domain.Common;

namespace CtrlBox.Domain.Entities
{
    public class SystemConfiguration : EntityBase
    {
        public string CultureInfo { get; set; }
        public string UnitProduct { get; set; }   
    }
}
