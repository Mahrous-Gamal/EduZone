using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{

    [Table("LikeForPostInGroup")]
    public class LikeForPostInGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}