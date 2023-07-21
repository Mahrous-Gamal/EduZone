using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models.ViewModels
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public int PostId { get; set; }
        public string NameOfCreatorPost { get; set; }
        public string ImageOfCreatorPost { get; set; }
        public string TypOfPost { get; set; }
        public string TimeOfNotifyAfterFormat { get; set; }
        public DateTime TimeOfNotifyBeforFormat { get; set; }

        public string GroupName { get; set; }
        public bool IsReaded { get; set; }

    }
}