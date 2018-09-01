using Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities;
using System;

namespace Acumatica.Support.Integration.Internal.OData
{
    public class CaseDTO : Case
    {
        public CaseDTO() : base() { }

        public DateTime DateReported { get; set; }

        public string Priority { get; set; }

        public string Severity { get; set; }

        public string Reason { get; set; }

        public DateTime? LastIncomingActivity { get; set; }

        public string ClassID { get; set; }
    }
}
