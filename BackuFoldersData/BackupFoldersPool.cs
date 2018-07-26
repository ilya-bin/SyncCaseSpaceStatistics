using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.BackupFoldersPool
{
    public static class BackupFoldersPool
    {
        static BackupFoldersPool()
        {
            RootFolder = new DirectoryInfo(ConfigurationManager.AppSettings["BackupFolderPath"]);
            BackupFolders = new List<BackupFolder>(
                RootFolder.GetDirectories().Select(d => new BackupFolder(d.Name)));
        }

        public static List<BackupFolder> BackupFolders { get; }

        private static DirectoryInfo RootFolder { get; }

        public static DirectoryInfo GetFolder(string folderName)
        {
            try
            {
                return RootFolder.GetDirectories().First(d => d.Name == folderName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}
