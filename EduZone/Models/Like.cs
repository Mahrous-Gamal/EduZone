using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduZone.Models
{
    public class Like
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}