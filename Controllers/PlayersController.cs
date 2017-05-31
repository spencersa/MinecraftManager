using Microsoft.AspNetCore.Mvc;
using MinecraftManager.Models;
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

        [HttpGet("[action]")]
        public async Task<IEnumerable<BannedIp>> GetBannedIps()
        {
            return (await FileReaderWriter.ReadJsonFileAsync("C:\\MinecraftData\\banned-ips.json")).ToObject<IEnumerable<BannedIp>>();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<BannedPlayer>> GetBannedPlayers()
        {
            return (await FileReaderWriter.ReadJsonFileAsync("C:\\MinecraftData\\banned-players.json")).ToObject<IEnumerable<BannedPlayer>>();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<UserCache>> GetUserCache()
        {
            return (await FileReaderWriter.ReadJsonFileAsync("C:\\MinecraftData\\usercache.json")).ToObject<IEnumerable<UserCache>>();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Player>> GetWhiteList()
        {
            return (await FileReaderWriter.ReadJsonFileAsync("C:\\MinecraftData\\whitelist.json")).ToObject<IEnumerable<Player>>();
        }
    }
}
