using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Dtos.Needed_Gears
{
    public record CreateNeededGearDto([Required] string Name, [Required] int Ammount);

}
