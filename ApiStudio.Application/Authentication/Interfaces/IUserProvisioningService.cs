using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiStudio.Application.Authentication.Models;

namespace ApiStudio.Application.Authentication.Interfaces
{
    public interface IUserProvisioningService
    {
        Task<Guid> ProvisionAsync(
            AuthenticationUser user,
            CancellationToken cancellationToken);
    }
}
