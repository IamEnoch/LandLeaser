using LandLeaser.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Interfaces
{
    public interface IRegisterService
    {
        public Task<bool> CreateUser(Register registerDetails);
    }
}
