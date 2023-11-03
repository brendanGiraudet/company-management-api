using CompanyManagement.API.EqualityComparers;
using CompanyManagement.API.Models;

namespace CompanyManagement.API.Repositories.Bill
{
    public class BillRepository : IBillRepository
    {
        private DatabaseContext _databaseContext;

        public BillRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<BillModel> createdBills)> CreateAsync(IEnumerable<BillModel> billModels)
        {
            using var dbContextTransaction = await _databaseContext.Database.BeginTransactionAsync();

            try
            {
                var newBills = billModels.Distinct(new BillEqualityComparer());

                newBills = GetUnexistedBills(newBills);

                _databaseContext.Bills.AddRange(newBills);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, newBills);
            }
            catch (Exception ex)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<BillModel>());
            }
        }

        private IEnumerable<BillModel> GetUnexistedBills(IEnumerable<BillModel> newBills)
        {
            var bills = new List<BillModel>();

            foreach (var bill in newBills)
            {
                if (_databaseContext.Bills.FirstOrDefault(f => f.Number == bill.Number) == null) bills.Add(bill);
            }

            return bills;
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, IEnumerable<BillModel> bills)> GetAsync()
        {
            try
            {
                return (StatusCodes.Status200OK, _databaseContext.Bills);
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, Enumerable.Empty<BillModel>());
            }
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, BillModel? bill)> GetAsync(string id)
        {
            try
            {
                return (StatusCodes.Status200OK, _databaseContext.Bills.FirstOrDefault(s => s.Id == id));
            }
            catch (Exception)
            {
                return (StatusCodes.Status500InternalServerError, null);
            }
        }

        /// <inheritdoc/>
        public async Task<(int statusCode, BillModel? updatedBill)> UpdateAsync(BillModel billModel)
        {
            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Update(billModel);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return (StatusCodes.Status201Created, billModel);
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();

                return (StatusCodes.Status500InternalServerError, null);
            }
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(string id)
        {
            var BillResult = await GetAsync(id);

            if (BillResult.bill == null) return StatusCodes.Status204NoContent;

            using var dbContextTransaction = _databaseContext.Database.BeginTransaction();

            try
            {
                _databaseContext.Remove(BillResult.bill);

                await _databaseContext.SaveChangesAsync();

                await dbContextTransaction.CommitAsync();

                return StatusCodes.Status200OK;
            }
            catch (Exception)
            {
                await dbContextTransaction.RollbackAsync();

                return StatusCodes.Status500InternalServerError;
            }
        }
    }
}