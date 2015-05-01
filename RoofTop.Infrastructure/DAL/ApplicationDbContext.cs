﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoofTop.Core.Entities;
using RoofTop.Core.DomainServices;
using RoofTop.Infrastructure.Migrations;

namespace RoofTop.Infrastructure.DAL
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext() : base("RoofTopDbConnection", throwIfV1Schema: false) { }
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

    }
}