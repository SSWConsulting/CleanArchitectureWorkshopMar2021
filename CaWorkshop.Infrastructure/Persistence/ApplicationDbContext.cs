using System;
using System.Reflection;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using CaWorkshop.Infrastructure.Identity;
using CaWorkshop.Infrastructure.Persistence.Interceptors;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CaWorkshop.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly AuditEntitiesSaveChangesInterceptor _auditEntitiesSaveChangesInterceptor;

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options,
                IOptions<OperationalStoreOptions> operationalStoreOptions,
                ICurrentUserService currentUserService)
            : base(options, operationalStoreOptions)
        {
            _auditEntitiesSaveChangesInterceptor =
                new AuditEntitiesSaveChangesInterceptor(currentUserService);
        }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditEntitiesSaveChangesInterceptor);

            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
