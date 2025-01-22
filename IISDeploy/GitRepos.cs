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
    public class GitRepos
    {
        CredentialService credentialService = new CredentialService();
        string Url = "";
        string targetName = "";
        Credential? Credential = null;

        public GitRepos(string gitUrl)
        {
            Url = gitUrl;
            GetTargetName();

        }

        public void GetTargetName()
        {
            Uri uri = new Uri(Url);
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);

            targetName = $"git:{baseUrl}";
        }

        public bool GetCredential()
        {
            Credential = credentialService.GetCredential(targetName);
            if (Credential == null)
            {
                return false;
            }
            return true;
        }

        public string Message = "";
        public bool Clone(string path, string BranchName)
        {
            try
            {
                if (!GetCredential())
                {
                    Message = "No credential found";
                    return false;
                }

                Repository.Clone(Url, path, new CloneOptions()
                {
                    BranchName = "Main",
                    FetchOptions = {
                    CredentialsProvider =  (url, usernameFromUrl, types) =>
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
    }
}
