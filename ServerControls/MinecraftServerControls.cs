using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MinecraftManager.ServerControls
{
    public class MinecraftServerControls
    {
        //TODO: Add class for server arguments
        private Process _process;
        private StreamWriter _writer;
        private StreamReader _output;
        private int processId;

        public MinecraftServerControls()
        {
        }

        public void StartServer(string serverFile, string serverPath)
        {
            var processInfo = new ProcessStartInfo("java", "-Xmx1024M -Xms1024M -jar " + serverFile + " nogui");
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardInput = true;
            processInfo.WorkingDirectory = serverPath;

            _process = Process.Start(processInfo);
            _writer = _process.StandardInput;
            processId = _process.Id;
            _process.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
            _process.WaitForExit();
        }

        void SortOutputHandler(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void SendCommand(string command)
        {
            _writer.WriteLine(command);
        }

        public void StopServer() => SendCommand("stop");
    }
}
