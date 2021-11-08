using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace KTU_API.Data.Entities
{
    public class Needed_Gear
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ammount { get; set; }
        public Path Path { get; set; }


        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
