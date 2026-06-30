using Microsoft.AspNetCore.Identity;

namespace ApiStudio.Infrastructure.Authentication.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
}