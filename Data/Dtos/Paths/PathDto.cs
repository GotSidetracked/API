using KTU_API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Dtos.Paths
{
    public record PathDto(int Id, string Name, string Description, List<Coordinate> PathCoordinates);
}
