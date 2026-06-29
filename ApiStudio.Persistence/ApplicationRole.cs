using Microsoft.AspNetCore.Identity;

namespace ApiStudio.Persistence;

public sealed class ApplicationRole : IdentityRole<Guid>
{

    public DateTimeOffset? LastLoginAt { get; set; }

    public bool IsExternalUser { get; set; }
}