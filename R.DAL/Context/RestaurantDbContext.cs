using Microsoft.EntityFrameworkCore;
using R.DAL.EntityModel;
using Common.Enum;

namespace R.DAL.Context
{
    public class RestaurantDbContext: DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options) { }

        public DbSet<CategoryModel> Category { get; set; } 
        public DbSet<MenuModel> Menu { get; set; }
        public DbSet<RoleModel> Role { get; set; }
        public DbSet<UserModel> Users { get; set; }

        //public DbSet<ForgotPasswordRequest> ForgotPasswordRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<RoleModel>().HasData(
            //    new RoleModel { RoleId = 1, RoleName = "Admin" },
            //    new RoleModel { RoleId = 2, RoleName = "User" }
            //);
        //    modelBuilder.Entity<RoleModel>().HasData(
        //               //new RoleModel { RoleId = (int) UserRole.Admin, RoleName = Enum.GetName(UserRole.Admin) },
        //               //new RoleModel { RoleId = (int) UserRole.User, RoleName = Enum.GetName(UserRole.User) }

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
