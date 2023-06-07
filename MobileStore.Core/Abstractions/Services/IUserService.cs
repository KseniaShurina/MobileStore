using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileStore.Core.Models;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Abstractions.Services
{
    public interface IUserService
    {
        Task<UserModel> GetCurrentUser();

        Task UpdateCurrentUser(UserModel model);
    }
}
