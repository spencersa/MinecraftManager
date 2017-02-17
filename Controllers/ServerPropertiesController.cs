﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MinecraftManager.Models;

namespace MinecraftManager.Controllers
{
    //TODO: make file path configurable
    [Route("api/[controller]")]
    public class ServerPropertiesController : Controller
    {
        [HttpGet("[action]")]
        public async Task<IEnumerable<ServerProperty>> GetServerPropertiesFile()
        {
            var returnValue = new List<ServerProperty>();
            var fileData = await FileReaderWriter.ReadAllLinesAsync("C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server\\server.properties");
            foreach (var data in fileData)
            {
                var index = data.IndexOf('=');
                if (index != -1)
                {
                    returnValue.Add(new ServerProperty
                    {
                        Property = data.Substring(0, index),
                        Value = data.Substring(index + 1)
                    });
                }
            }
            return returnValue;
        }

        [HttpPost("[action]")]
        public async Task<bool> UpdateServerProperties([FromBody] ServerProperty property)
        {
            List<ServerProperty> serverProperties = new List<ServerProperty>();
            var returnValue = new List<ServerProperty>();
            var currentFileLines = await GetServerPropertiesFile();

                foreach (var currentProperty in currentFileLines)
                {
                    if (currentProperty.Property == property.Property && currentProperty.Value != property.Value)
                    {
                        currentProperty.Value = property.Value;
                    }
                }
            return await FileReaderWriter.WriteFileAsync("C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server\\server.properties", ToStringList(currentFileLines.ToList()));
        }

        private List<string> ToStringList(List<ServerProperty> list)
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