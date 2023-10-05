using CompanyManagement.API.Models;
using System.Diagnostics.CodeAnalysis;

namespace CompanyManagement.API.EqualityComparers
{
    public class ClientEqualityComparer : IEqualityComparer<ClientModel>
    {
        public bool Equals(ClientModel? x, ClientModel? y)
        {
            return x?.Name == y?.Name && x?.Email == y?.Email;
        }

        public int GetHashCode([DisallowNull] ClientModel obj)
        {
            return base.GetHashCode();
        }
    }
}
