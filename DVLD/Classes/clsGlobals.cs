using DVLDBusinessLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Classes
{
    public class clsGlobals
    {
       static string  keyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\DVLD";

        public static clsUser CurrentUser {  get; set; }
        private static string Encrypt(string txt, char key = 'k')
        {
            StringBuilder Result = new StringBuilder();
            foreach (char c in txt)
            {
                Result.Append((char)(c ^ key));
            }
            return Result.ToString();
        }
        private static string Decrypt(string txt, char key = 'k')
        {
            return Encrypt(txt, key);
        }
        //to registry
        public static bool RememberUserNameAndPassword(string username, string password)
        {
            try
            {
                Registry.SetValue(keyPath, "UserName", username);
                Registry.SetValue(keyPath, "Password", Encrypt(password));
                return true;
            }
            catch { return false; }
        }
        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                username = Registry.GetValue(keyPath, "UserName", null) as string;
                string EncPassword = Registry.GetValue(keyPath, "Password", null) as string;
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(EncPassword))
                    return false;
                else
                    password = Decrypt(EncPassword);
            }
            catch { return false; }
            return true;
        }
    }
}
