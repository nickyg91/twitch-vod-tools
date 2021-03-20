using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch.Vod.Services.Models
{
    public enum HttpVerb : byte
    {
        POST,
        GET,
        PUT,
        DELETE,
        PATCH,
    }
}
