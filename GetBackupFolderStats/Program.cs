using Acumatica.Support.Programs.SyncCaseSpaceStatistics.BackupFoldersPool;
using Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities;
using System;
using System.Linq;

namespace Acumatica.Support.Programs.GetBackupFolderStats
{
    class Program
    {
        static void Main(string[] args)
        {
            var backupFolders = BackupFoldersPool.BackupFolders;

            var currentTime = DateTime.Now;
            using (var db = new DbSpacesContext())
            {
                db.Database.ExecuteSqlCommand("DELETE [BackupFoldersDetail]");

                db.BackupFoldersDetails.AddRange(backupFolders.Select(b => BackupFolderToDbEntity(b)));
                db.SaveChanges();
            }
        }

        static BackupFoldersDetail BackupFolderToDbEntity(BackupFolder backupFolder)
        {
            return new BackupFoldersDetail()
            {
                FolderName = backupFolder.Name
                ,
                CaseID = backupFolder.CaseID
                ,
                SizeMb = Math.Round(backupFolder.Size / 1024 / 1024, 2)
                ,
                SyncDateTime = CurrentTime
            };
        }

        static Program()
        {
            CurrentTime = DateTime.Now;
        }

        static DateTime CurrentTime;
    }
}
