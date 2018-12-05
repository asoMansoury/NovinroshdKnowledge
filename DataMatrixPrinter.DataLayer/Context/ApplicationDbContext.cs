using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using DataMatrixPrinter.Common.Extensions;
using DataMatrixPrinter.DomainClasses.Entities.Business;
using DataMatrixPrinter.DomainClasses.Entities.Identites;
using DataMatrixPrinter.DomainClasses.Mapping.Business;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataMatrixPrinter.DataLayer.Context
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
        IUnitOfWork
    {

        #region Contractor

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region Entities
        //public virtual IDbSet<Customer> Customers { get; set; }
        #endregion

        #region UnitOfWork

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            AuditFields();
            return base.SaveChanges();
        }

        public Task<int> SaveAllChangesAsync()
        {
            AuditFields();
            return base.SaveChangesAsync();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Added;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            this.Database.Initialize(force: true);
        }

        private void AuditFields()
        {
            var dbEntityEntries =
                this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified);
            foreach (var dbEntityEntry in dbEntityEntries)
            {
                var edmMembers =
                    ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(
                        dbEntityEntry.Entity)
                    .EntitySet.ElementType.Members.ToList();

                if (dbEntityEntry.State == EntityState.Added)
                {
                    var createdOn = edmMembers.Any(d => d.Name == "CreatedOn") ? dbEntityEntry.Property("CreatedOn") : null;
                    if (createdOn != null)
                    {
                        createdOn.CurrentValue = DateTime.Now;
                    }

                    var createdIp = edmMembers.Any(d => d.Name == "CreatedIp") ? dbEntityEntry.Property("CreatedIp") : null;
                    if (createdIp != null)
                    {
                        createdIp.CurrentValue = HttpContext.Current.GetIp();
                    }

                    var createdById = edmMembers.Any(d => d.Name == "CreatedById") ? dbEntityEntry.Property("CreatedById") : null;
                    if (createdById != null)
                    {
                        createdById.CurrentValue = HttpContext.Current.GetUserId();
                    }

                }
            }
        }
        #endregion

        #region OnModelCreating

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<CustomRole>().ToTable("Roles");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            //modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new CountryConfig());
            modelBuilder.Configurations.Add(new CityConfig());
            
        }
        #endregion
    }
}