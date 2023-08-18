using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "6f89b8be-56c1-4cf7-ae35-6ed140586872";
            var writerRoleId = "d90056aa-4944-4e6a-8812-2c81e8494c56";


            // Create a 2 Roles (Reader (user) & Writer(Admin))
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };
            //Seed the Roles
            // Way to seeding is using builder object
            builder.Entity<IdentityRole>().HasData(roles);


            //Create an Admin User
            var adminUserId = "4463593b-6f7a-4686-b9a1-53917da17ed9";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@demo.com",
                Email = "admin@demo.com",
                NormalizedEmail = "admin@demo.com".ToUpper(),
                NormalizedUserName = "admin@demo.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);
            //Give Roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new ()
                {
                    UserId=adminUserId,
                    RoleId=readerRoleId
                },
                 new ()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId
                }
            };


            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);



        }


    }
}
