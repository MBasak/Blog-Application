using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.DbContexts
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "112ef104-fb22-4dfa-88a5-ad618801e6b0";
            var superAdminRoleId = "e81a896c-a58c-4d53-8407-d44b62037684";
            var userRoleId = "80342440-04ab-42b1-9017-4762d913d7f0";


            // Seed roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name= "Admin",
                    NormalizedName="Admin",
                    Id= adminRoleId,
                    ConcurrencyStamp= adminRoleId
                },

                new IdentityRole
                {
                    Name= "SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id= superAdminRoleId,
                    ConcurrencyStamp= superAdminRoleId
                },

                new IdentityRole
                {
                    Name= "User",
                    NormalizedName="User",
                    Id= userRoleId,
                    ConcurrencyStamp= userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // create super admin user 
            var superAdminUserId = "a6e8080d-951f-4806-b7b6-7ea85cd5edec";
            var superAdminUser = new IdentityUser
            {
                UserName = "John",
                Email = "abc@gmail.com",
                NormalizedEmail = "superAdmin@blogger.com",
                NormalizedUserName = "John",
                Id = superAdminUserId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "SuperAdmin123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            //Add all roles to superAdmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId= superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId= superAdminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId= superAdminUserId
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);



        }
    }
}
