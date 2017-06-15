using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Models
{
    public class BanInfo
    {
        public DateTime Created { get; set; }
        public string CreatedFormatted { get; set; }
        //TODO: Make this an enum?
        public string Source { get; set; }
        //TODO: can this be a date? Right now file says "forever"
        public string Expires { get; set; }
        public string Reason { get; set; }
    }
}
