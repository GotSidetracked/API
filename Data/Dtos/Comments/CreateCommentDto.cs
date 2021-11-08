using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Dtos.Comments
{
    public record CreateCommentDto([Required] string Name, [Required] string Body);
}
