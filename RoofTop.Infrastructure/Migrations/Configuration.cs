namespace RoofTop.Infrastructure.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RoofTop.Core.Entities;
    using RoofTop.Infrastructure.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<RoofTop.Infrastructure.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedAdminUsers(context);
            SeedCities(context);
        }

        /// <summary>
        /// Seed admin user
        /// </summary>
        /// <param name="context"></param>
        private void SeedAdminUsers(ApplicationDbContext context) 
        {
            var adminuser = "admin1@gmail.com";
            var userRole = "admin";
            var pwd = "abc123";

            if (!context.Roles.Any(r => r.Name == userRole))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = userRole };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == adminuser ))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = adminuser };

                manager.Create(user, pwd);
                manager.AddToRole(user.Id, userRole);
            }

        }
        /// <summary>
        /// Seed Metro Manila cities
        /// </summary>
        /// <param name="context"></param>
        private void SeedCities(ApplicationDbContext context)
        {
            IEnumerable<City> cities = new List<City>
            {
                new City { Id = 1, Name = "Manila", Region = "NCR" },
                new City { Id = 2, Name = "Caloocan", Region = "NCR" },
                new City { Id = 3, Name = "Las Pinas", Region = "NCR" },
                new City { Id = 4, Name = "Makati", Region = "NCR" },
                new City { Id = 5, Name = "Malabon", Region = "NCR" },
                new City { Id = 6, Name = "Mandaluyong", Region = "NCR" },
                new City { Id = 7, Name = "Markina", Region = "NCR" },
                new City { Id = 8, Name = "Muntinlupa", Region = "NCR" },
                new City { Id = 9, Name = "Navotas", Region = "NCR" },
                new City { Id = 10, Name = "Paranaque", Region = "NCR" },
                new City { Id = 11, Name = "Pasay", Region = "NCR" },
                new City { Id = 12, Name = "Pasig", Region = "NCR" },
                new City { Id = 13, Name = "Quezon", Region = "NCR" },
                new City { Id = 14, Name = "San Juan", Region = "NCR" },
                new City { Id = 15, Name = "Taguig", Region = "NCR" },
                new City { Id = 16, Name = "Valenzuela", Region = "NCR" },
                new City { Id = 17, Name = "Pateros", Region = "NCR" },

            };

            foreach (var city in cities)
            {
                if ( !context.Cities.Any(c=>c.Id == city.Id) )
                {
                    context.Cities.Add(city);
                }
            }
            context.SaveChanges();
        }

    }
}
