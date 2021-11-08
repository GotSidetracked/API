using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KTU_API.Autherization.Model;

namespace KTU_API.Data.Entities
{
    public class Path : IUserOwnedResource
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        [Required]
        public List<Coordinate> PathCoordinates { get; set;}


        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
