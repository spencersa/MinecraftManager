using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("[action]")]
        public void Start()
        {
            int exitCode;
            ProcessStartInfo processInfo;
            Process process;
            Console.WriteLine("start");
            processInfo = new ProcessStartInfo
            {
                FileName = "C:\\Users\\Holy Shit Awesome\\Desktop\\Minecraft Server\\Run.bat",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            // Warning: This approach can lead to deadlocks, see Edit #2
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Dispose();
        }
    }
}
