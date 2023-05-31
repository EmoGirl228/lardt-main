﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetById(int id);

        Task Create(User Model);

        Task Update(User Model);

        Task Delete(int id);
        Task<User> Login(string email, string password);
    }
}
