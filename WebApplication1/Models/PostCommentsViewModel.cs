﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PostCommentsViewModel
    {
        public Post Post { get; set; }

        public List<Comment> Comments { get; set; }
        
        public int PostID { get; set; }

        public string Content { get; set; }

        [Required, MinLength(2), MaxLength(256)]
        [Display(Name = "Comment")]
        public string CommentContent { get; set; }
    }
}
