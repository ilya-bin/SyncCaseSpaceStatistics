namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BackupFoldersDetail")]
    public partial class BackupFoldersDetail
    {
        [Key]
        [StringLength(255)]
        public string FolderName { get; set; }

        [StringLength(10)]
        public string CaseID { get; set; }

        public double SizeMb { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SyncDateTime { get; set; }
    }
}
