using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models
{
    public class Post
    {
        [Required, Display(Name = "Post ID")]
        public int ID { get; set; }

        [Required, Display(Name = "Publish Date")]
        public DateTime Published { get; set; }

        [Display(Name = "User")]
        public String Username { get; set; }

        [Required, MinLength(6), MaxLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required, MinLength(6), MaxLength(1024)]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}
