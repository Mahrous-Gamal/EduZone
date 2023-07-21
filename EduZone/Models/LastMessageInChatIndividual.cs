using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class LastMessageInChatIndividual
    {
        public int Id { get; set; }
        public string SendId { get; set; }
        public string ReseverID { get; set; }
        public DateTime LastMessage { get; set; }
    }
}