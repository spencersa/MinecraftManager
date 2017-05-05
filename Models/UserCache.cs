using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Models
{
    public class UserCache : Player
    {
        public DateTime ExpiresOn { get; set; }
    }
}
