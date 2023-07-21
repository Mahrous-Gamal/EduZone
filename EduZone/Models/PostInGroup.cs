using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EduZone.Models
{

    [Table("PostInGroup")]
    public class PostInGroup
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "Content must be between 1 and 500 characters", MinimumLength = 1)]
        public string ContentOfPost { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        public string GroupId { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Likes")]
        public virtual ICollection<LikeForPostInGroup> LikeForPostInGroup { get; set; }

    }
}