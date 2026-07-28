using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class clsSharedUtil
    {
        public static string GenarateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public static string ReplaseFileNameWithGUID(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo file = new FileInfo(FileName);
            string extn = file.Extension;
            return GenarateGUID() + extn;
        }
        public static bool CreateFolderIfNotExist(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    string Location = "clsUtil → CreateFolderIfNotExist";
                    clsEventLogger.LogEvent(ex, Location, System.Diagnostics.EventLogEntryType.Error);
                    //return false;
                    throw ;

                }
            }
            return true;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:DVLD-People-Images\";
            if (!CreateFolderIfNotExist(DestinationFolder))
            {
                return false;
            }
            string destinationFile = DestinationFolder + ReplaseFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, destinationFile);
            }
            catch (IOException iox)
            {
                string Location = "clsUtil → CopyImageToProjectImagesFolder";
                clsEventLogger.LogEvent(iox, Location, System.Diagnostics.EventLogEntryType.Error);
               // return false;
               throw iox;
            }
            sourceFile = destinationFile;
            return true;


        }
    }
}
