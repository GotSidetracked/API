using KTU_API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KTU_API.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Create(User user);
        Task<User> Put(User user);
        Task Delete(User user);
    }

  
    public class UserRepository : IUserRepository
    {
        private readonly DBContextModel contextModel;
    
        public UserRepository(DBContextModel ContextModel)
        {
           contextModel = ContextModel;    
        }

        public async Task<User> Create(User user)
        {
            contextModel.users.Add(user);
            await contextModel.SaveChangesAsync();
            return user;
        }

        public async Task Delete(User user)
        {
            contextModel.users.Remove(user);
            await contextModel.SaveChangesAsync();
            return;
        }

        public async Task<User> Get(int id)
        {
            return await contextModel.users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return contextModel.users.ToList();
        }

        public async Task<User> Put(User user)
        {
            contextModel.users.Update(user);
            await contextModel.SaveChangesAsync();
            return user;
        }
    }
}

