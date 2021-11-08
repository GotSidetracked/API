using KTU_API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Data.Repositories
{
    public interface IPathRepository
    {
        Task<IEnumerable<Path>> GetAll();
        Task<Path> Get(int id);
        Task<Path> Create(Path path);
        Task<Path> Put(Path path);
        Task Delete(Path path);
    }


    public class PathRepository : IPathRepository
    {
        private readonly DBContextModel dbContextModel;

        public PathRepository(DBContextModel DBContextModel)
        {
            dbContextModel = DBContextModel;
        }

        public async Task<Path> Create(Path path)
        {
            dbContextModel.paths.Add(path);
            await dbContextModel.SaveChangesAsync();
            return path;
        }

        public async Task Delete(Path path)
        {
             dbContextModel.paths.Remove(path);
             await dbContextModel.SaveChangesAsync();
            return;
        }

        public async Task<Path> Get(int id)
        {
            return await dbContextModel.paths.FindAsync(id);
        }

        public async Task<IEnumerable<Path>> GetAll()
        {
            return dbContextModel.paths.ToList();
        }

        public async Task<Path> Put(Path path)
        {
            dbContextModel.paths.Update(path);
            await dbContextModel.SaveChangesAsync();
            return path;
        }
    }
}
