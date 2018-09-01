using Acumatica.Support.Integration.Internal.OData;
using Acumatica.Support.Programs.SyncCaseSpaceStatistics.DbEntities;
using System;
using System.Threading.Tasks;

namespace Acumatica.Support.Programs.GetCaseStatsFromInternal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var currentTime = DateTime.Now;
            // Get cases from internal
            var internalClient = new InternalOdataClient();
            var cases = await internalClient.GetAllCasesFromInternalGIAsync();

            // update database
            using (var db = new DbSpacesContext())
            {
                db.Database.ExecuteSqlCommand("DELETE [Cases]");

                
                cases.ForEach(c => c.SyncDateTime = currentTime);
                db.Cases.AddRange(cases);
                db.SaveChanges();
            }
        }       
    }
}
