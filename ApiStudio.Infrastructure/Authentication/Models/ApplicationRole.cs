using Microsoft.AspNetCore.Identity;

namespace ApiStudio.Infrastructure.Authentication.Models;

public sealed class ApplicationRole : IdentityRole<Guid>
{

    public DateTimeOffset? LastLoginAt { get; set; }

    public bool IsExternalUser { get; set; }
}