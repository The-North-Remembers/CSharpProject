using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectCSharp.Models
{
    public class EditPostViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public DateTime Date { get; set; }

        public string AuthorId { get; set; }

        public Post Save()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Post post = db.Posts.Find(Id);
            post.Id = Id;
            post.Title = Title;
            post.Body = Body;
            post.Date = Date;
            post.Author = db.Users.Where(u => u.Id == AuthorId).FirstOrDefault();
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            return post;
        }
    }
}