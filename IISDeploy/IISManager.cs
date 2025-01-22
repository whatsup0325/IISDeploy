using Microsoft.Web.Administration;
using LibGit2Sharp;
using IISDeploy.BuildStrategy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace IISDeploy
{
    public class IISManager
    {
        ServerManager serverManager = new ServerManager();
        IBuildStrategy? buildStrategy = null;

        public event Action<string>? StatusChanged;

        private string _status = "";
        public string Status
        {
            get => _status;
            private set
            {
                _status = value;
                StatusChanged?.Invoke(_status); // 通知狀態變更
            }
        }

        public void UseNodeJsBuild(string outputPath)
        {
            this.buildStrategy = new NodeJsBuildStrategy(outputPath);
            buildStrategy.OutputStringChanged += (output) =>
            {
                Status = output;
            };
        }

        public void UseDotNetBuild(string projectFile, string outputPath)
        {
            this.buildStrategy = new DotNetBuildStrategy(projectFile, outputPath);
            buildStrategy.OutputStringChanged += (output) =>
            {
                Status = output;
            };

        }

        // 列出所有 IIS 站點         
        public List<Site> ListIISWebSites()
        {
            return serverManager.Sites.ToList();
        }

        public void DeployWithoutPause(string gitUrl, string siteName, string path, string branch)
        {
            DeployGitCode(gitUrl, siteName, path, branch, false);
        }

        public void DeployWithPause(string gitUrl, string siteName, string path, string branch)
        {
            DeployGitCode(gitUrl, siteName, path, branch, true);
        }

        private void DeployGitCode(string gitUrl, string siteName, string path, string branch, bool isPause)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Status = "Cloning repository...";

            var gitRepo = new GitRepos(gitUrl);
            try
            {
                var cloneResult = gitRepo.Clone(tempDir, branch);
                if (!cloneResult)
                {
                    Status = $"Clone failed: {gitRepo.Message}";
                    return;
                }
            }
            finally
            {
                // 嘗試釋放相關資源
                gitRepo.Dispose();
            }

            Status = "Updating files...";

            if (buildStrategy == null)
            {
                Status = "Deploy Fail : No build strategy set";
                throw new Exception("No build strategy set.");
            }

            BuildManager buildManager = new BuildManager(buildStrategy);
            string outputPath = buildManager.ExecuteBuild(tempDir);

            ServerManager serverManager = new ServerManager();
            if (isPause)
            {
                var site = serverManager.Sites[siteName];
                site.Stop();
            }

            // 直接覆蓋檔案
            DirectoryCopy(outputPath, path, true);

            if (isPause)
            {
                var site = serverManager.Sites[siteName];
                site.Start();
            }

            // 清理暫存目錄
            DeleteDirectory(tempDir);

            Status = "Deploy Success";
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            var files = Directory.GetFiles(directoryPath);
            var directories = Directory.GetDirectories(directoryPath);

            foreach (var file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (var dir in directories)
            {
                DeleteDirectory(dir);
            }

            File.SetAttributes(directoryPath, FileAttributes.Normal);

            Directory.Delete(directoryPath, false);
        }


        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // 確保目標目錄存在
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            // 複製檔案
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);

                // 覆蓋檔案
                file.CopyTo(tempPath, true);
            }

            // 如果需要，遞迴處理子目錄
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }
}
