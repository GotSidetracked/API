using KTU_API.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KTU_API.Data.Dtos.Paths
{
    public record CreatePathDto([Required] string Name, [Required] string Description, [Required] List<Coordinate> PathCoordinates);
}
