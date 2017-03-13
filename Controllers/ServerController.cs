using Microsoft.AspNetCore.Mvc;
using MinecraftManager.Models;
using MinecraftManager.ServerControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        string serverPath = "C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server";
        string serverFile = "minecraft_server.1.11.2.jar";

        private readonly MinecraftServerControls _serverControl;
        public ServerController(MinecraftServerControls serverControl)
        {
            _serverControl = serverControl;
        }

        [HttpGet("[action]")]
        public void Start()
        {
            _serverControl.StartServer(serverFile, serverPath);
        }

        [HttpGet("[action]")]
        public void Stop()
        {
            _serverControl.StopServer();
        }

        [HttpGet("[action]")]
        public ServerMessages GetServerOutput()
        {
            return _serverControl.GetServerOutput();
        }

        [HttpPost("[action]")]
        public bool SendCommand([FromBody] string command)
        {
            Exception exception;
            try
            {
                _serverControl.SendCommand(command);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
