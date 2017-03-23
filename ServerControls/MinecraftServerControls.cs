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
        private List<Player> _players;

        public MinecraftServerControls()
        {
            _serverMessages = new ServerMessages();
            _players = new List<Player>();
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
            _process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
            _process.WaitForExit();
        }

        void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                _serverMessages.Messages.Add(e.Data);
                HandleOutput(e.Data);
            }
        }

        public ServerMessages GetServerOutput()
        {
            return _serverMessages;
        }

        public void SendCommand(string command)
        {
            _writer.WriteLine(command);
        }

        public void HandleOutput(string output)
        {
            UpdateServerStatus(output);
            UpdatePlayersList(output);
        }

        public void UpdatePlayersList(string output)
        {
            if (output.Contains("[User Authenticator #1/INFO]"))
            {
                const string playerNamePrefix = "player ";
                var playerNameIndex = output.IndexOf(playerNamePrefix);
                _players.Add(new Player
                {
                    Id = Guid.Parse(
                        output.Substring(playerNameIndex + playerNamePrefix.Length)
                        .Split(new char[0])[2]),
                    UserName = output.Substring(playerNameIndex + playerNamePrefix.Length)
                        .Split(new char[0])[0]
                });
            }
        }

        public void UpdateServerStatus(string output)
        {
            if (_serverMessages.ServerStatus == "Starting..." && output.Contains("Done"))
            {
                _serverMessages.ServerStatus = "Online";
            }
            else if (_serverMessages.ServerStatus == "Online" && output.Contains("Stopping"))
            {
                _serverMessages.ServerStatus = "Offline";
            }
        }

        public void StopServer() => SendCommand("stop");

        public void RestartServer() => SendCommand("restart");
    }
}
