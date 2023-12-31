﻿using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Client
{
    public interface IClientRepository
    {
        /// <summary>
        /// Create multiple client
        /// </summary>
        /// <param name="clientModels"></param>
        /// <returns>Status code and the error message if not empty</returns>
        Task<(int statusCode, IEnumerable<ClientModel> createdClients)> CreateAsync(IEnumerable<ClientModel> clientModels);

        /// <summary>
        /// Get client from database
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, IEnumerable<ClientModel> clients)> GetAsync();
        
        /// <summary>
        /// Get client from database
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, ClientModel? client)> GetAsync(string id);
        
        /// <summary>
        /// Update client
        /// </summary>
        /// <returns></returns>
        Task<(int statusCode, ClientModel updatedClient)> UpdateAsync(ClientModel clientModel);

        /// <summary>
        /// Delete client
        /// </summary>
        /// <returns></returns>
        Task<int> DeleteAsync(string id);
    }
}
