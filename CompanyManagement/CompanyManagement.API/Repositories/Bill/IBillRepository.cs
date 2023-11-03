using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Bill
{
    public interface IBillRepository
    {
        /// <summary>
        /// Create multiple Bill
        /// </summary>
        /// <param name="BillModels"></param>
        /// <returns>Status code and Bills</returns>
        Task<(int statusCode, IEnumerable<BillModel> createdBills)> CreateAsync(IEnumerable<BillModel> billModels);


        /// <summary>
        /// Get Bills
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, IEnumerable<BillModel> bills)> GetAsync();
        
        /// <summary>
        /// Get Bill
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, BillModel? bill)> GetAsync(string id);
        
        /// <summary>
        /// Update Bill
        /// </summary>
        /// <returns>Status code</returns>
        Task<(int statusCode, BillModel? updatedBill)> UpdateAsync(BillModel billModel);


        /// <summary>
        /// Delete Bill
        /// </summary>
        /// <returns>Status code</returns>
        Task<int> DeleteAsync(string id);
    }
}