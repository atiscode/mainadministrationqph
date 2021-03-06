﻿using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using Sieve.Models;
using Sieve.Services;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<User> GetUsers(SieveModel sieveModel);
        Task<User> GetUser(int id);
        Task<User> InsertUser(User user);
        Task<User> GetLoginByCredentials(UserLogin userLogin);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> CheckDuplicatedEmail(string email);
        Task<bool> CheckDuplicatedPhone(string phone);
        Task<bool> CheckDuplicatedNickname(string nickname);
        Task<bool> ActivateUserAccount(string code);
        Task<bool> CheckActivationCode(string code);
        Task<UserDetailDto> GetUserDetail(int userId);
        void SendMail(string code, string destination, string message);
        string GenerateActivationCode(int length = 48, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789");
    }
}