using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain;

namespace EmoBase.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(EmoBaseContext repositoryContext)
            : base (repositoryContext)
        {
        }
        public async Task<User?> GetByEmailAndPassword(string role, string password)
        {
            var result = await base.FindByCondition(x => x.Role1 == role && x.Password1 == password);
            if (result != null || result.Count == 0) 
            {
                return null;
            }
            return result[0];
        }
    }
}
