namespace Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Case
    {
        [StringLength(10)]
        public string CaseID { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ClosingDate { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [StringLength(50)]
        public string OwnerName { get; set; }

        [StringLength(50)]
        public string SecondaryOwner { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastModifiedOn { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SyncDateTime { get; set; }
    }
}
