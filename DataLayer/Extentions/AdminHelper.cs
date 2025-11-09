using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Threading.Tasks;
namespace DataLayer.Extensions
{
    public static class AdminHelper
    {
        public static async Task CreateAdminDefaultAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>()
                {
                    new User ()
                        {
                            Username = "Admin",
                            PasswordHash = new PasswordHasher<User>().HashPassword(null, "Pn1234567!"),
                            Role = "Admin",
                            RefreshToken = null,
                            RefreshTokenExpiryTime = null
                        },
                        new User ()
                        {
                            Username = "PhanNguyet",
                            PasswordHash = new PasswordHasher<User>().HashPassword(null, "Hp9876521!"),
                            Role = "RBAC",
                            RefreshToken = null,
                            RefreshTokenExpiryTime = null
                        }
                };
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
        }
    }
}
