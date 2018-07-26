using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.BackupFoldersPool
{
    public class BackupFolder
    {
        public BackupFolder(string name)
        {
            Name = name;
            this.DirectoryInfo = BackupFoldersPool.GetFolder(Name);
            if (this.DirectoryInfo == null)
            {
                throw new ArgumentNullException($"Unable to find or access folder with the name {this.Name}");
            }

            var casePatternMatch = new Regex(ConfigurationManager.AppSettings["CaseIdPattern"]).Match(Name);
            if (casePatternMatch.Success)
            {
                CaseID = casePatternMatch.Value;
            }
        }


        public string Name { get; }

        public string FullName => this.DirectoryInfo.FullName;

        private DirectoryInfo DirectoryInfo { get; }

        public string CaseID { get; set; }

        public double Size => DirectoryInfo.GetSize();
    }
}
