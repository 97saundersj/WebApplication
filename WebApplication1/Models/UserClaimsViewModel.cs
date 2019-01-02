using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserClaimsViewModel
    {
        [Display(Name = "User")]
        public String Username { get; set; }

        [Display(Name = "Can Create Posts")]
        public Boolean canCreatePosts { get; set; }

    }
}
