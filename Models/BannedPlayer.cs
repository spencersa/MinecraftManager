using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Models
{
    public class BannedPlayer : BanInfo
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
    }
}
