using MinecraftManager.Models;
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
        private int processId;
        private ServerMessages _serverMessages;

        public MinecraftServerControls()
        {
            _serverMessages = new ServerMessages();
        }

        public void StartServer(string serverFile, string serverPath)
        {
            _serverMessages.ServerStatus = "Starting...";
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
            if (e.Data != null)
            {
                _serverMessages.Messages.Add(e.Data);
                if (e.Data.Contains("Done"))
                {
                    _serverMessages.ServerStatus = "Online";
                }
                else if (e.Data.Contains("Stopping"))
                {
                    _serverMessages.ServerStatus = "Offline";
                }
            }
        }

        //TODO: make this better
        public ServerMessages GetServerOutput()
        {
            if (_serverMessages != null)
            {
                return _serverMessages;
            }
            else
            {
                return new ServerMessages();
            }
        }

        public void SendCommand(string command)
        {
            _writer.WriteLine(command);
        }

        public void StopServer() => SendCommand("stop");
    }
}
