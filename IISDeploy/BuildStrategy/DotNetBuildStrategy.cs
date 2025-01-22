using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISDeploy.BuildStrategy
{
    public class DotNetBuildStrategy : IBuildStrategy
    {
        public event Action<string>? OutputStringChanged;

        private readonly string _projectFile;
        private readonly string _outputPath;

        public DotNetBuildStrategy(string projectFile, string outputPath)
        {
            _projectFile = projectFile;
            _outputPath = outputPath;
        }
        public string Build(string sourcePath)
        {
            Console.WriteLine("Executing .NET build for specific project...");

            if (string.IsNullOrEmpty(_projectFile))
            {
                throw new ArgumentException("Project file path must be provided.");
            }

            string projectPath = Path.Combine(sourcePath, _projectFile);

            if (!Directory.Exists(projectPath))
            {
                throw new FileNotFoundException($"Project file not found: {projectPath}");
            }

            // 執行 dotnet build 指定的專案
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c dotnet build \"{projectPath}\"",
                WorkingDirectory = sourcePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        OutputStringChanged?.Invoke(e.Data);
                    }
                };

                // 註冊錯誤事件
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        OutputStringChanged?.Invoke(e.Data);
                    }
                };

                process.Start();


                // 開始非同步讀取輸出和錯誤流
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception(".NET build failed.");
                }
            }

            
            string binPath = Path.Combine(sourcePath,_projectFile, _outputPath);
            if (!Directory.Exists(binPath))
            {
                OutputStringChanged?.Invoke("Build output directory not found: bin");
                throw new DirectoryNotFoundException("Build output directory not found: bin");
            }

            return binPath;
        }
    }
}
