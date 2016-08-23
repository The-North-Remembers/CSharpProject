using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectCSharp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }
    }
}