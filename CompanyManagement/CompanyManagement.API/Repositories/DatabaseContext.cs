using CompanyManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.API.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientModel>().HasMany(n => n.Addresses).WithOne(i => i.Client).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AddressTypeModel>().HasMany(n => n.Addresses).WithOne(i => i.AddressType).OnDelete(DeleteBehavior.SetNull);

        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{ClientModel}"/>.
        /// </summary>
        public virtual DbSet<ClientModel> Clients { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{AddressModel}"/>.
        /// </summary>
        public virtual DbSet<AddressModel> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{AddressTypeModel}"/>.
        /// </summary>
        public virtual DbSet<AddressTypeModel> AddressTypes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{ServiceModel}"/>.
        /// </summary>
        public virtual DbSet<ServiceModel> Services { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="DbSet{BillModel}"/>.
        /// </summary>
        public virtual DbSet<BillModel> Bills { get; set; }
    }
}
