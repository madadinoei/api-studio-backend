namespace ApiStudio.Infrastructure.Exceptions;

public sealed class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("نام کاربری و رمز عبور صحیح نمی باشد.")
    {
    }
}