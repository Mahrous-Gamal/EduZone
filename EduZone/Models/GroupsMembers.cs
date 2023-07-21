using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class GroupsMembers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string GroupId { get; set; }
        public DateTime TimeGoin { get; set; }
        public string MemberId { get; set; }

        public bool IsCreate { get; set; }

    }
}