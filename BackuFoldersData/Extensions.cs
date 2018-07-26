using System.IO;

namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.BackupFoldersPool
{
    public static class Extensions
    {
        public static double GetSize(this DirectoryInfo directory)
        {
            double size = 0;

            FileInfo[] fis = directory.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }

            DirectoryInfo[] dis = directory.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += di.GetSize();
            }
            return size;
        }
    }
}
