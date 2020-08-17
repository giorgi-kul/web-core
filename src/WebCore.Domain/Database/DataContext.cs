using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WebCore.Domain.Entities;
using WebCore.Domain.Interfaces;

namespace WebCore.Domain.Database
{
    public class DataContext : ApiAuthorizationDbContext<Administrator>
    {
        private readonly ICurrentUserService _currentUserService;

        public DataContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AdministratorAction> AdministratorActions { get; set; }
        public DbSet<Log> Logs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = this.Administrators.FindAsync(_currentUserService.UserId.Value).Result;
                        entry.Entity.CreateDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = this.Administrators.FindAsync(_currentUserService.UserId.Value).Result;
                        entry.Entity.LastModifyDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
