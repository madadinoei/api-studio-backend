using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiStudio.Domain.Enums
{
    public enum BodyType
    {
        None,
        Raw,
        Json,
        Xml,
        FormData,
        UrlEncoded,
        Binary
    }
}
