using System;
using System.IO;
using System.Windows.Forms;


namespace DVLD.Classes
{
    public class clsUtil
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
        public static bool CreateFolderIfNotExist( string FolderPath)
        {
            if(!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        public static bool CopyImageToProjectImagesFolder(ref string sourceFile)
        {
            string DestinationFolder = @"C:DVLD-People-Images\";
            if(!CreateFolderIfNotExist(DestinationFolder))
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
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = destinationFile;
            return true;
        

        }
    }
}
