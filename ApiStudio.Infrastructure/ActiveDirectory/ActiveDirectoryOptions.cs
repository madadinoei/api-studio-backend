namespace ApiStudio.Infrastructure.ActiveDirectory;

public sealed class ActiveDirectoryOptions
{
    public const string SectionName = "ActiveDirectory";

    public string Server { get; set; } = default!;

    public int Port { get; set; } = 389;

    public string Domain { get; set; } = default!;

    public string BaseDn { get; set; } = default!;
}