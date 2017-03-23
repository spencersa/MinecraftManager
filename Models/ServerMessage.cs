using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Models
{
    public class ServerMessages
    {
        public ServerMessages()
        {
            Messages = new List<string>();
            ServerStatus = "Offline";
        }
        public List<string> Messages { get; set; }
        public string ServerStatus { get; set; }
        public List<Player> Players { get; set; }
    }
}
