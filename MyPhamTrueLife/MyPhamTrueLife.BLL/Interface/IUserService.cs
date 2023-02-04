﻿using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(Login value);
        Task<InfoUser> ResgisterUserAsync(RegisterUser value);
        Task<int> ForgotPassWordAsync(string UserName);
        Task<bool> CheckUserNameAsync(string userName);
        Task<InfoUser> GetByUserNameAsync(string userName);
    }
}
