using System;

namespace CtrlBox.UI.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        
        //excptionDetail.Path;
        //excptionDetail.Error.Message;
        //excptionDetail.Error.Source;
        //excptionDetail.Error.StackTrace;
    }
}