using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpControls.AutoAddress.Backend
{
    public record Response
    {
        public string StatusText { get; init; }
        public JToken? Content { get; init; }
    }
}
