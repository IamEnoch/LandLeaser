using LandLeaserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Interfaces
{
    public interface IUserService
    {
        public Task<UserBasicInfo> GetUser(string email);
    }
}
