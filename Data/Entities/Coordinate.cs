using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KTU_API.Data.Entities
{
    public class Coordinate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double XCord { get; set; }
        [Required]
        public double YCord { get; set; }


        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
