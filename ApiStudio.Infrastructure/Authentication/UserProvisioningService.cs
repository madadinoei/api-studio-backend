using ApiStudio.Application.Authentication.Interfaces;
using ApiStudio.Application.Authentication.Models;
using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiStudio.Infrastructure.Authentication;

public sealed class UserProvisioningService : IUserProvisioningService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;

    public UserProvisioningService(
        UserManager<ApplicationUser> userManager,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task ProvisionAsync(
        AuthenticationUser externalUser,
        CancellationToken cancellationToken)
    {
        // ---------- Identity ----------

        var identityUser =
            await _userManager.FindByNameAsync(externalUser.UserName);

        if (identityUser is null)
        {
            identityUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = externalUser.UserName,
                Email = externalUser.Email
            };

            var result = await _userManager.CreateAsync(identityUser);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(Environment.NewLine,
                        result.Errors.Select(e => e.Description)));
            }
        }
        else
        {
            var changed = false;

            if (identityUser.Email != externalUser.Email)
            {
                identityUser.Email = externalUser.Email;
                changed = true;
            }

            if (changed)
            {
                var result = await _userManager.UpdateAsync(identityUser);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(Environment.NewLine,
                            result.Errors.Select(e => e.Description)));
                }
            }
        }

        // ---------- Domain ----------

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.UserName == externalUser.UserName,
                cancellationToken);

        if (user is null)
        {
            user = User.Create(
                externalUser.UserName,
                externalUser.DisplayName,
                externalUser.Email,identityUser.Id);

            _context.Users.Add(user);
        }
        else
        {
            user.UpdateProfile(
                externalUser.DisplayName,
                externalUser.Email);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}