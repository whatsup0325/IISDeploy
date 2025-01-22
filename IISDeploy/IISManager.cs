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
            GitRepos GitRepos = new GitRepos(gitUrl);
            var cloneResult = GitRepos.Clone(tempDir, branch);
            if (!cloneResult)
            {
                Status = $"Clone failed:{GitRepos.Message}";
                return;
            }
            else
            {
                Status = "Clone success!";
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


            if (Directory.Exists(path))
            {
                Status = $"Updating files in {path}...";
                Directory.Delete(path, true);
            }

            DirectoryCopy(outputPath, path, true);

            if (isPause)
            {
                var site = serverManager.Sites[siteName];
                site.Start();
            }


            Directory.Delete(outputPath, true);

            Status = "Deploy Success";
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // 複製目錄及其內容
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

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
