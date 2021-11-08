using KTU_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Repositories
{
    public interface IGearRepository
    {
        Task<Needed_Gear> GetAsync(int PathId, int GearId);
        Task<List<Needed_Gear>> GetAsync(int PathId);
        Task InsertAsync(Needed_Gear gear);
        Task UpdateAsync(Needed_Gear gear);
        Task DeleteAsync(Needed_Gear gear);
    }

    public class GearRepository : IGearRepository
    {
       private readonly DBContextModel dbContextModel;

        public GearRepository(DBContextModel DBContextModel)
        {
            dbContextModel = DBContextModel;
        }

        public async Task<Needed_Gear> GetAsync(int PathId, int GearId)
        {
            return await dbContextModel.needed_Gears.FirstOrDefaultAsync(o => o.Path.Id == PathId && o.Id == GearId);
        }

        public async Task<List<Needed_Gear>> GetAsync(int PathId)
        {
            return await dbContextModel.needed_Gears.Where(o => o.Path.Id == PathId).ToListAsync();
        }

        public async Task InsertAsync(Needed_Gear gear)
        {
            dbContextModel.needed_Gears.Add(gear);
            await dbContextModel.SaveChangesAsync();
        }

        public async Task UpdateAsync(Needed_Gear gear)
        {
            dbContextModel.needed_Gears.Update(gear);
            await dbContextModel.SaveChangesAsync();
        }

        public async Task DeleteAsync(Needed_Gear gear)
        {
            dbContextModel.needed_Gears.Remove(gear);
            await dbContextModel.SaveChangesAsync();
        }
    }
}
