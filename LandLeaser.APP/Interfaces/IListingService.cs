using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.Shared.DTOs;

namespace LandLeaserApp.Interfaces
{
    public interface IListingService
    {
        /// <summary>
        /// Method that gets all listings
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <returns></returns>
        public Task <List<GetListingDto>> GetListingsAsync (string authToken = null);

        /// <summary>
        /// Gets a specific listing
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="id">Listing id</param>
        /// <returns></returns>
        public Task<GetListingDto> GetListingAsync(string authToken, string id);
    }
}
