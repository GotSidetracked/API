using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KTU_API.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string UserID { get; set; }

        public DateTime CreationDateUtc { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<Comment> Responses { get; set; }
        public Path Path { get; set; }
        public User User { get; set; }

    }
}
