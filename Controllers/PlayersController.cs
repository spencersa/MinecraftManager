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
        public async Task<IEnumerable<UserCache>> GetUserCache()
        {
            var returnValue = new List<UserCache>();
            var fileData = await FileReaderWriter.ReadAllLinesAsync("C:\\MinecraftData\\usercache.json");
            foreach (var data in fileData)
            {
                var index = data.IndexOf('=');
                if (index != -1)
                {
                    returnValue.Add(new UserCache
                    {
                        //Id = data.Substring(0, index),
                        //UserName = data.Substring(index + 1)
                    });
                }
            }
            return returnValue;
        }
    }
}
