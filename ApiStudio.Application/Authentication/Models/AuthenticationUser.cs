using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStudio.Application.Authentication.Models
{
    public sealed class AuthenticationUser
    {
        public required string UserName { get; init; }

        public required string DisplayName { get; init; }

        public string? Email { get; init; }

        public string? Department { get; init; }
    }
}
