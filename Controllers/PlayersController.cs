using Microsoft.AspNetCore.Mvc;
using MinecraftManager.ServerControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly PlayerControls _playerControl;
        public PlayersController(PlayerControls playerControl)
        {
            _playerControl = playerControl;
        }
    }
}
