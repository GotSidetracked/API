using KTU_API.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Dtos.Users
{
    public record UpdateUserDto([Required] int id, [Required] string name, [Required] string password);
}
