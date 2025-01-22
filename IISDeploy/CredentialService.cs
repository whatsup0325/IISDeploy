using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IISDeploy
{
    public class CredentialService
    {
        public Credential? GetCredential(string targetName)
        {
            IntPtr credPtr;
            bool result = CredRead(targetName, CRED_TYPE.GENERIC, 0, out credPtr);

            if (!result)
            {
                return null; // 未找到憑證
            }

            var credential = Marshal.PtrToStructure<CREDENTIAL>(credPtr);

            // 將 IntPtr 轉換為 string
            string userName = Marshal.PtrToStringUni(credential.UserName);
            string password = Marshal.PtrToStringUni(credential.CredentialBlob, (int)credential.CredentialBlobSize / 2);

            CredFree(credPtr);

            return new Credential
            {
                UserName = userName,
                Password = password
            };
        }

        //public Credential PromptForCredential()
        //{
        //    using (var loginForm = new GitLoginForm())
        //    {
        //        if (loginForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            return new Credential
        //            {
        //                UserName = loginForm.Username,
        //                Password = loginForm.Password
        //            };
        //        }
        //    }
        //    return null; // 如果使用者取消
        //}

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredRead(string target, CRED_TYPE type, int reservedFlag, out IntPtr credential);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern void CredFree([In] IntPtr buffer);

        private enum CRED_TYPE : int
        {
            GENERIC = 1,
            DOMAIN_PASSWORD = 2,
            DOMAIN_CERTIFICATE = 3,
            DOMAIN_VISIBLE_PASSWORD = 4,
            GENERIC_CERTIFICATE = 5,
            DOMAIN_EXTENDED = 6,
            MAXIMUM = 7,
            MAXIMUM_EX = MAXIMUM + 1000,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public int Flags;
            public CRED_TYPE Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public IntPtr LastWritten;
            public int CredentialBlobSize;
            public IntPtr CredentialBlob;
            public int Persist;
            public int AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        public class Credential
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
