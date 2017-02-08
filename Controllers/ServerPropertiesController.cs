using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftManager.Controllers
{
    [Route("api/[controller]")]
    public class ServerPropertiesController : Controller
    {
        [HttpGet("[action]")]
        public async Task<IEnumerable<TestString>> GetServerPropertiesFile()
        {
            var returnValue = new List<TestString>();
            var fileData = await FileReaderWriter.ReadAllLinesAsync("C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server\\server.properties");
            foreach (var data in fileData)
            {
                var index = data.IndexOf('=');
                if (index != -1)
                {
                    returnValue.Add(new TestString {
                        Property = data.Substring(0, index),
                        Value = data.Substring(index + 1)
                    });
                }
            }
            return returnValue;
        }

        [HttpGet("[action]")]
        public async Task<bool> UpdateServerProperties(List<TestString> serverProperties)
        {
            var returnValue = new List<TestString>();
            var currentFileLines = await GetServerPropertiesFile();

            foreach (var newProperty in serverProperties)
            {
                foreach(var currentProperty in currentFileLines)
                {
                    if (currentProperty.Property == newProperty.Property && currentProperty.Value != newProperty.Value)
                    {
                        currentProperty.Value = newProperty.Value;
                    }  
                }
            }
            return await FileReaderWriter.WriteFileAsync("C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server\\server.properties", ToStringList(currentFileLines.ToList()));
        }

        private List<string> ToStringList(List<TestString> list)
        {
            var returnList = new List<string>();
            foreach (var testString in list)
            {
                returnList.Add($"{testString.Property}={testString.Value}");
            }
            return returnList;
        }

        public class TestString
        {
            public string Property { get; set; }
            public string Value { get; set; }
        }
    }
}
