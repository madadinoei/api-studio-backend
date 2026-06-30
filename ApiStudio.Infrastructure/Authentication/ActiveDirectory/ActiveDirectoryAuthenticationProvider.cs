using System.DirectoryServices.Protocols;
using System.Net;
using ApiStudio.Application.Authentication.Interfaces;
using ApiStudio.Application.Authentication.Models;
using Microsoft.Extensions.Options;

namespace ApiStudio.Infrastructure.ActiveDirectory;

public sealed class ActiveDirectoryAuthenticationProvider
    : IExternalAuthenticationProvider
{
    private readonly ActiveDirectoryOptions _options;

    public ActiveDirectoryAuthenticationProvider(
        IOptions<ActiveDirectoryOptions> options)
    {
        _options = options.Value;
    }

    public async Task<AuthenticationUser?> AuthenticateAsync(
        string userName,
        string password,
        CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            using var connection = new LdapConnection(
                new LdapDirectoryIdentifier(
                    _options.Server,
                    _options.Port));

            connection.AuthType = AuthType.Negotiate;

            connection.Credential = new NetworkCredential(
                userName,
                password,
                _options.Domain);

            try
            {
                connection.Bind();
            }
            catch (LdapException)
            {
                return null;
            }

            var request = new SearchRequest(
                _options.BaseDn,
                $"(sAMAccountName={userName})",
                SearchScope.Subtree,
                new[]
                {
                    "displayName",
                    "mail",
                    "sAMAccountName",
                    "department"
                });

            var response =
                (SearchResponse)connection.SendRequest(request);

            if (response.Entries.Count == 0)
                return null;

            var entry = response.Entries[0];

            return new AuthenticationUser
            {
                UserName = Get(entry, "sAMAccountName") ?? userName,
                DisplayName = Get(entry, "displayName") ?? userName,
                Email = Get(entry, "mail"),
                Department = Get(entry, "department")
            };

        }, cancellationToken);
    }

    private static string? Get(
        SearchResultEntry entry,
        string attribute)
    {
        if (!entry.Attributes.Contains(attribute))
            return null;

        return entry.Attributes[attribute][0]?.ToString();
    }
}