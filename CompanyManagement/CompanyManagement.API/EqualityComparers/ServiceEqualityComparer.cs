using CompanyManagement.API.Models;
using System.Diagnostics.CodeAnalysis;

namespace CompanyManagement.API.EqualityComparers
{
    public class ServiceEqualityComparer : IEqualityComparer<ServiceModel>
    {
        public bool Equals(ServiceModel? x, ServiceModel? y)
        {
            return x?.Name == y?.Name;
        }

        public int GetHashCode([DisallowNull] ServiceModel obj)
        {
            return base.GetHashCode();
        }
    }
}
