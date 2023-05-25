using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IReviewBase
    {
        Task Update(User Model);
        Task Delete(User Model);
        Task Create(User model);
    }
}
