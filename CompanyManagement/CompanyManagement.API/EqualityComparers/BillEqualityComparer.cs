using CompanyManagement.API.Models;
using System.Diagnostics.CodeAnalysis;

namespace CompanyManagement.API.EqualityComparers
{
    public class BillEqualityComparer : IEqualityComparer<BillModel>
    {
        public bool Equals(BillModel? x, BillModel? y)
        {
            return x?.Number == y?.Number;
        }

        public int GetHashCode([DisallowNull] BillModel obj)
        {
            return base.GetHashCode();
        }
    }
}
