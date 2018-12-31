using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Comment
    {
        [Required, Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "User")]
        public String Username { get; set; }

        [Required, MinLength(6), MaxLength(256)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        public virtual Post MyPost { get; set; }
    }
}
