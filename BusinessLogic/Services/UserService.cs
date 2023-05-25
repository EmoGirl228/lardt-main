﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.Services.UserService;
using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindAll();
        }
        public async Task<User> GetById(int id)
        {
            var user = await _repositoryWrapper.User.FindByCondition(x => x.Userid == id);
            return user.First();
        }
        public async Task Create(User Model)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model));
            }
            if (string.IsNullOrEmpty(Model.Login1))
            {
                throw new ArgumentException(nameof(Model.Login1));
            }
            if (string.IsNullOrEmpty(Model.Password1))
            {
                throw new ArgumentException(nameof(Model.Password1));
            }
            if (string.IsNullOrEmpty(Model.Role1))
            {
                throw new ArgumentException(nameof(Model.Role1));
            }
            await _repositoryWrapper.User.Create(Model);
            await _repositoryWrapper.Save();
        }
        public async Task Update(User Model)
        {
            _repositoryWrapper.User.Update(Model);
            _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User.FindByCondition(x => x.Userid == id);

            _repositoryWrapper.User.Delete(user.First());
            _repositoryWrapper.Save();
        }
    }
}


