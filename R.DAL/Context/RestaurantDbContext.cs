using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.DAL.EntityModel;

namespace R.DAL.Context
{
    public class RestaurantDbContext: DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<MenuModel> Menu { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { RoleId = 1, RoleName = "Admin" },
                new RoleModel { RoleId = 2, RoleName = "User" }
            );
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryId = 1, CategoryName = "Appetizers" },
                 new CategoryModel { CategoryId = 2, CategoryName = "Main Course" },
                new CategoryModel { CategoryId = 3, CategoryName = "Desserts" }
                );
            modelBuilder.Entity<UserModel>()
               .HasIndex(u => u.Username)
               .IsUnique();

            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        }
    }
}
