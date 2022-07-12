using Lab_Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Product Brand
                if (!context.ProductBrands.Any())
                {
                    context.ProductBrands.AddRange(new List<ProductBrand>()
                    {
                        new ProductBrand()
                        {
                            Name = "Samsung",
                            Logo = "/Images/LogoSamsung.jpg"
                        },
                        new ProductBrand()
                        {
                            Name = "Apple",
                            Logo = "/Images/LogoApple.png"
                        },
                        new ProductBrand()
                        {
                            Name = "Dell",
                            Logo = "/Images/LogoDell.png"
                        },
                        new ProductBrand()
                        {
                            Name = "HP",
                            Logo = "/Images/LogoHP.png"
                        },
                        new ProductBrand()
                        {
                            Name = "LG",
                            Logo = "/Images/LogoLG.png"
                        },
                    });
                    context.SaveChanges();
                }
                //Product
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Laptop",
                            Price = 4100m,
                            Image = "/Images/LaptopDell.jpg",
                            ProductBrandId = 3
                        },
                        new Product()
                        {
                            Name = "Samsung S20",
                            Price = 2150.50m,
                            Image = "/Images/SamsungS20.jpg",
                            ProductBrandId = 1
                        },
                        new Product()
                        {
                            Name = "TV",
                            Price = 2999.99m,
                            Image = "/Images/TvLG.jpg",
                            ProductBrandId = 5
                        },
                        new Product()
                        {
                            Name = "IPhone X",
                            Price = 2499.90m,
                            Image = "/Images/iPhoneX.jpg",
                            ProductBrandId = 2
                        },
                        new Product()
                        {
                            Name = "IPhone 12",
                            Price = 3399m,
                            Image = "/Images/iPhone12.jpg",
                            ProductBrandId = 2
                        },
                        new Product()
                        {
                            Name = "Monitor",
                            Price = 990.49m,
                            Image = "/Images/MonitorSamsung.jpg",
                            ProductBrandId = 1
                        },
                        new Product()
                        {
                            Name = "Printer",
                            Price = 329m,
                            Image = "/Images/PrinterHP.jpg",
                            ProductBrandId = 4
                        }
                    });
                    context.SaveChanges();
                }
            }

        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@onlineshop.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = adminUserEmail,
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin123@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@onlineshop.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "App User",
                        UserName = appUserEmail,
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User123@");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
