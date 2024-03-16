﻿using KTPS.Model.Entities.User;
using System.Threading.Tasks;

namespace KTPS.Model.Repositories.User;

public interface IUserRepository
{
    Task<UserBasic> GetByUsernameAsync(string username);
    Task<UserBasic> GetByEmailAsync(string email);
    Task InserAsync(UserBasic user);
}