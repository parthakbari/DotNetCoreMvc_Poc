using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HegicMvc_Poc.Models
{
    public class NameValueData
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public int ParentId { get; set; }

        public string OriginalName { get; set; }
    }
}
