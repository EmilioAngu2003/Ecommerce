using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Data;

public class ApplicationDbContextSeed
{
    public static async Task SeedRolesAndUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<ApplicationDbContextSeed> logger)
    {
        const string superAdminRole = "SuperAdmin";
        const string superAdminUserName = "superadmin";
        const string superAdminUserEmail = "superadmin@ecommerce.com";
        const string superAdminUserPassword = "passwordSuperAdmin123!";

        var roleExists = await roleManager.RoleExistsAsync(superAdminRole);
        if (roleExists)
        {
            logger.LogInformation("SuperAdmin role already exists.");
        }
        else
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole(superAdminRole));

            if (!roleResult.Succeeded)
            {
                logger.LogError("Failed to create SuperAdmin role.");
                return;
            }

            logger.LogInformation($"Role SuperAdmin created successfully.");

        }

        var superAdmin = await userManager.FindByEmailAsync(superAdminUserEmail);
        if (superAdmin != null)
        {
            logger.LogInformation("SuperAdmin user already exists.");
        }
        else
        {
            superAdmin = new IdentityUser
            {
                UserName = superAdminUserName,
                Email = superAdminUserEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(superAdmin, superAdminUserPassword);
            if (!createResult.Succeeded)
            {
                logger.LogError("Failed to create SuperAdmin user.");
                return;
            }

            logger.LogInformation("SuperAdmin user created successfully.");
        }

        var hasRole = await userManager.IsInRoleAsync(superAdmin, superAdminRole);
        if (hasRole)
        {
            logger.LogInformation("SuperAdmin user already assigned to role.");
        }
        else
        {
            var roleAssignmentResult = await userManager.AddToRoleAsync(superAdmin, superAdminRole);
            if (!roleAssignmentResult.Succeeded)
            {
                logger.LogError("Failed to assign SuperAdmin role to user.");
                return;
            }

            logger.LogInformation($"SuperAdmin user assigned to role SuperAdmin.");
        }
    }
}
