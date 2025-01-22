using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IISDeploy.CredentialService;

namespace IISDeploy
{
    public class GitRepos : IDisposable
    {
        private readonly CredentialService credentialService = new CredentialService();
        private string Url = "";
        private string targetName = "";
        private Credential? Credential = null;
        private bool disposed = false;

        public string Message { get; private set; } = "";

        public GitRepos(string gitUrl)
        {
            Url = gitUrl;
            GetTargetName();
        }

        private void GetTargetName()
        {
            Uri uri = new Uri(Url);
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);
            targetName = $"git:{baseUrl}";
        }

        private bool GetCredential()
        {
            Credential = credentialService.GetCredential(targetName);
            return Credential != null;
        }

        public bool Clone(string path, string branchName)
        {
            try
            {
                if (!GetCredential())
                {
                    Message = "No credential found";
                    return false;
                }

                Repository.Clone(Url, path, new CloneOptions
                {
                    BranchName = branchName,
                    FetchOptions = {
                    CredentialsProvider = (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials
                        {
                            Username = Credential!.UserName,
                            Password = Credential!.Password
                        }
                }
                });
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        // 實現 IDisposable 接口
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                // 如果有其他非受控資源需要釋放，可以在這裡處理
                disposed = true;
            }
        }

        ~GitRepos()
        {
            Dispose(false);
        }
    }
}
