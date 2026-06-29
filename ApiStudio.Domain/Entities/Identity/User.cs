using ApiStudio.Domain.Common;

namespace ApiStudio.Domain.Entities.Identity;

public class User : BaseEntity
{
    public string UserName { get; private set; }

    public string DisplayName { get; private set; }

    public string? Email { get; private set; }

    private User()
    {
    }

    public static User Create(
        string userName,
        string displayName,
        string? email)
    {
       ///
       return new User();
    }

    public void UpdateProfile(
        string displayName,
        string? email)
    {
       ///

    }
}