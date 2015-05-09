using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using RoofTop.Infrastructure.Migrations;
using RoofTop.Core.ApplicationServices;


namespace RoofTop.Infrastructure.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _curUserService;
        
        public ApplicationDbContext() : base("RoofTopDbConnection", throwIfV1Schema: false) {}

        //Unity will call this constructor by default
        public ApplicationDbContext(ICurrentUserService curUserService) : this()
        {
            _curUserService = curUserService;
        }

        public static ApplicationDbContext Create() { return new ApplicationDbContext(); }
        
        public IDbSet<RealEstateAd> RealEstateAds { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Used by AppHarbor to automatically update database
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
            
        }

        //Override method to automagically update CreateDate, CreatedBy, ModifiedDate, ModifiedDate every save
        public override int SaveChanges()
        {
            foreach (var auditableEntity in ChangeTracker.Entries<IAuditable>())
            {
                if (auditableEntity.State == EntityState.Added ||
                    auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.ModifiedDate = DateTime.Now;
                    auditableEntity.Entity.ModifiedBy = _curUserService.UserID;

                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreateDate = DateTime.Now;
                        auditableEntity.Entity.CreatedBy = _curUserService.UserID;
                    }
                    else
                    {
                        // we also want to make sure that code is not inadvertly
                        // modifying created date and created by columns 
                        auditableEntity.Property(p => p.CreateDate).IsModified = false;
                        auditableEntity.Property(p => p.CreatedBy).IsModified = false;
                    }
                }
            }

            return base.SaveChanges();

        }
    }
}