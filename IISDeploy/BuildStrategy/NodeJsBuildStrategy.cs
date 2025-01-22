using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISDeploy.BuildStrategy
{
    public class NodeJsBuildStrategy : IBuildStrategy
    {
        public event Action<string>? OutputStringChanged;

        public string Build(string sourcePath)
        {
            Console.WriteLine("Executing Node.js build...");

            // 執行 npm install 和 npm run build
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c npm install && npm run build",
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

                // 等待命令完成
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Command failed with exit code {process.ExitCode}");
                }
            }

            // 假設輸出在 dist 資料夾
            string distPath = Path.Combine(sourcePath, "dist");
            if (!Directory.Exists(distPath))
            {
                throw new DirectoryNotFoundException("Build output directory not found: dist");
            }

            return distPath;
        }
    }

}
