using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MinecraftManager.Models;
using MinecraftManager.ServerControls;

namespace MinecraftManager.Controllers
{
    //TODO: make file path configurable
    //TODO: Create and inject a new service into this controller
    [Route("api/[controller]")]
    public class ServerPropertiesController : Controller
    {
        [HttpGet("[action]")]
        public async Task<IEnumerable<ServerPropertys>> GetServerPropertiesFile()
        {
            var returnValue = new List<ServerPropertys>();
            var fileData = await FileReaderWriter.ReadAllLinesAsync("C:\\MinecraftData\\server.properties");
            foreach (var data in fileData)
            {
                var index = data.IndexOf('=');
                if (index != -1)
                {
                    returnValue.Add(new ServerPropertys
                    {
                        Property = data.Substring(0, index),
                        Value = data.Substring(index + 1)
                    });
                }
            }
            return returnValue;
        }

        [HttpPost("[action]")]
        public async Task<bool> UpdateServerProperties([FromBody] ServerPropertys property)
        {
            List<ServerPropertys> serverProperties = new List<ServerPropertys>();
            var returnValue = new List<ServerPropertys>();
            var currentFileLines = await GetServerPropertiesFile();

                foreach (var currentProperty in currentFileLines)
                {
                    if (currentProperty.Property == property.Property && currentProperty.Value != property.Value)
                    {
                        currentProperty.Value = property.Value;
                    }
                }
            return await FileReaderWriter.WriteFileAsync("C:\\MinecraftData\\server.properties", ToStringList(currentFileLines.ToList()));
        }

        private List<string> ToStringList(List<ServerPropertys> list)
        {
            var returnList = new List<string>();
            foreach (var testString in list)
            {
                returnList.Add($"{testString.Property}={testString.Value}");
            }
            return returnList;
        }
    }
}
