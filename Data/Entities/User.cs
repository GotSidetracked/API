using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace KTU_API.Data.Entities
{
    /*        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }*/
    /*[Required]
    public DateTime dateCreated { get; set; }
    [Required]
    public Role role { get; set; }
    */
    public class User : IdentityUser
    {
        public List<Comment> userComments { get; set; }

        List<Path> userPaths { get; set; }

        public enum Role : ushort
        {
            Admin = 0,
            User = 1,
            Visitor = 2
        }
    }

}
